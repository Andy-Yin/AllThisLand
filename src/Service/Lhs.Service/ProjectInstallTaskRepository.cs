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
    /// 安装任务
    /// </summary>
    public class ProjectInstallTaskRepository : PlatformBaseService<T_ProjectInstallTask>, IProjectInstallTaskRepository
    {
        public ProjectInstallTaskRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectInstallTask>> GetInstallTaskList(int projectId, int secondCategoryId)
        {
            string sql = @$"
select task.*
            from T_ProjectInstallTask task
                left
            join T_ProjectMaterialItem item
                on task.ProjectMaterialItemId = item.Id
            WHERE task.ProjectId = {projectId}
            and item.secondCategoryId = {secondCategoryId}
            AND task.IsDel = 0
            and item.IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectInstallTask>(sql)).ToList();

            return list;
        }

        public async Task<List<T_ProjectInstallTask>> GetInstallTaskListByProjectAndUserId(int projectId, int userId)
        {
            string sql = @$"select *  from T_ProjectInstallTask where ProjectId = {projectId} and UserId = {userId} and IsDel = 0";
            var list = (await this.AllAsync(sql)).ToList();
            return list;
        }

        public async Task<bool> SubmitTask(
            List<T_ProjectInstallTask> taskList,
            IProjectMaterialItemLogRepository logRepository,
            IProjectMaterialRepository projectMaterialRepository,
            string timeSign,
            string key)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 更新U9审批流状态回写
                        var client = new JFClient();
                        var detailIdList = new List<string>();
                        foreach (var task in taskList)
                        {
                            // 创建任务
                            var taskId = await this.AddAsync(conn, task, tran);

                            // 更新主物料状态
                            var materialItem = await projectMaterialRepository.SingleAsync(task.ProjectMaterialItemId);
                            // 数量是所有已收货的数量
                            var quantity = materialItem.Quantity;
                            MaterialMachine.Install(ref materialItem);
                            await projectMaterialRepository.UpdateAsync(conn, materialItem, tran);

                            // 添加log记录
                            var log = new T_ProjectMaterialItemLog();
                            log.CreateTime = DateTime.Now;
                            log.ProjectMaterialItemId = task.ProjectMaterialItemId;
                            log.Remark = "提交安装任务";
                            log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), materialItem.Status);
                            log.Status = materialItem.Status;

                            await logRepository.AddAsync(conn, log, tran);

                            detailIdList.Add(materialItem.DetailsId);
                        }

                        var response = await client.SetInstallGoodsAsync(CommonConst.U9ServiceSource, timeSign, key, detailIdList.ToArray());
                        var u9Result = JsonHelper.DeserializeJsonToObject<JFSendProStatus>(response);
                        if (u9Result.errcode == 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
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
    }
}
