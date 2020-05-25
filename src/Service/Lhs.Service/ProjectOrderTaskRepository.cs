using System;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Dapper;
using Dapper.Contrib.Extensions;
using Lhs.Common.Const;
using Lhs.Entity.ForeignDtos.Response.U9;
using Lhs.Helper;
using Microsoft.Extensions.Configuration;
using U9Service;

namespace Lhs.Service
{
    /// <summary>
    /// 订单任务
    /// </summary>
    public class ProjectOrderTaskRepository : PlatformBaseService<T_ProjectOrderTask>, IProjectOrderTaskRepository
    {
        public ProjectOrderTaskRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectOrderTask>> GetOrderTaskList(int projectId, int secondCategoryId)
        {
            string sql = @$"
select task.*
            from T_ProjectOrderTask task
                left
            join T_ProjectMaterialItem item

                on task.ProjectMaterialItemId = item.Id
            WHERE task.ProjectId = {projectId}
            and item.secondCategoryId = {secondCategoryId}
            AND task.IsDel = 0
            and item.IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectOrderTask>(sql)).ToList();

            return list;
        }

        public async Task<List<T_ProjectOrderTask>> GetOrderTaskListByProjectId(int projectId)
        {
            string sql = @$"
select task.*
            from T_ProjectOrderTask task
                left
            join T_ProjectMaterialItem item

                on task.ProjectMaterialItemId = item.Id
            WHERE task.ProjectId = {projectId}
            AND task.IsDel = 0
            and item.IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectOrderTask>(sql)).ToList();

            return list;
        }

        public async Task<List<T_ProjectOrderTask>> GetOrderTaskListByProjectAndUserId(int projectId, int userId)
        {
            string sql = @$"select *  from T_ProjectOrderTask where ProjectId = {projectId} and UserId = {userId} and IsDel = 0";
            var list = (await this.AllAsync(sql)).ToList();
            return list;
        }

        public async Task<bool> SubmitOrderTask(
            string projectNo,
            string remark,
            int orderType,
            int supplier,
            List<T_ProjectOrderTask> taskList,
            IProjectMaterialItemLogRepository logRepository,
            IProjectMaterialRepository projectMaterialRepository,
            string timeSign,
            string key,
            int userId)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var detailIdList = new List<string>();
                        foreach (var task in taskList)
                        {
                            // 创建任务
                            var taskId = await this.AddAsync(conn, task, tran);

                            // 更新主物料状态
                            var materialItem = await projectMaterialRepository.SingleAsync(task.ProjectMaterialItemId);
                            MaterialMachine.SubmitOrder(ref materialItem);
                            await projectMaterialRepository.UpdateAsync(conn, materialItem, tran);

                            // 添加log记录
                            var log = new T_ProjectMaterialItemLog();
                            log.CreateTime = DateTime.Now;
                            log.ProjectMaterialItemId = task.ProjectMaterialItemId;
                            log.Remark = "提交订单任务";
                            log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), materialItem.Status);
                            log.Status = materialItem.Status;

                            await logRepository.AddAsync(conn, log, tran);

                            detailIdList.Add(materialItem.DetailsId);
                        }

                        // 更新U9审批流状态回写
                        var client = new JFClient();
                        var response = await client.SetPlaceOrder2Async(CommonConst.U9ServiceSource, timeSign, key, detailIdList.ToArray());
                        var u9Result = JsonHelper.DeserializeJsonToObject<JFSendProStatus>(response);
                        if (u9Result.errcode == 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> BatchUpdateOrderFromU9(
            List<T_ProjectOrderTask> taskList,
            IProjectMaterialRepository projectMaterialRepository,
            IProjectMaterialItemLogRepository logRepository,
            List<T_ProjectMaterialItem> materialList)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 更新物料
                        await projectMaterialRepository.UpdateListAsync(conn, materialList, tran);
                        // 更新订单
                        await UpdateListAsync(conn, taskList, tran);

                        foreach (var task in taskList)
                        {
                            // 添加log记录
                            var log = new T_ProjectMaterialItemLog();
                            log.CreateTime = DateTime.Now;
                            log.ProjectMaterialItemId = task.ProjectMaterialItemId;
                            log.Remark = "订单确认";
                            log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), materialList.First().Status);
                            log.Status = materialList.First().Status;

                            await logRepository.AddAsync(conn, log, tran);
                        }
                       

                        tran.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
