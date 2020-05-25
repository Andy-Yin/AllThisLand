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
using Lhs.Entity.DbEntity;
using Lhs.Entity.ForeignDtos.Response.U9;
using Lhs.Helper;
using Microsoft.Extensions.Configuration;
using U9Service;

namespace Lhs.Service
{
    /// <summary>
    /// 测量任务
    /// </summary>
    public class ProjectMeasureTaskRepository : PlatformBaseService<T_ProjectMeasureTask>, IProjectMeasureTaskRepository
    {
        public ProjectMeasureTaskRepository(IConfiguration config)
        {
            Config = config;
        }
        public async Task<List<ProjectMeasureTask>> GetMeasureTaskList(int projectId, int secondCategoryId)
        {
            string sql = @$"
select task.id, task.Status, item.MaterialName,  item.Space
from T_ProjectMeasureTask task
         left join T_ProjectMaterialItem item
                   on item.Id = task.ProjectMaterialItemId
where task.IsDel = 0
  and item.IsDel = 0
  and item.SecondCategoryId = {secondCategoryId}
  and item.ProjectId = {projectId}";
            var list = (await this.FindAllAsync<ProjectMeasureTask>(sql)).ToList();

            return list;
        }

        public async Task<List<T_ProjectMeasureTask>> GetMeasureTaskListByProjectAndUserId(int projectId, int userId)
        {
            string sql = @$"select *  from T_ProjectMeasureTask where ProjectId = {projectId} and UserId = {userId} and IsDel = 0";
            var list = (await this.AllAsync(sql)).ToList();
            return list;
        }

        public async Task<bool> SubmitTask(
            List<T_ProjectMeasureTask> taskList,
            List<SubMeasureTaskItem> taskItemList,
            IProjectMaterialItemLogRepository logRepository,
            IProjectMeasureTaskItemRepository projectMeasureTaskItemRepository,
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
                        var listForU9 = new List<MaInfo>();
                        // 操作所有的任务
                        foreach (var task in taskList)
                        {
                            // 创建任务
                            var taskId = await this.AddAsync(conn, task, tran);

                            var taskItemListFroDb = new List<T_ProjectMeasureTaskItem>();
                            foreach (var itemFromReq in taskItemList)
                            {
                                if (itemFromReq.ProjectMaterialItemId == task.ProjectMaterialItemId)
                                {
                                    var taskItem = new T_ProjectMeasureTaskItem();
                                    taskItem.Amount = itemFromReq.Amount;
                                    taskItem.Size = itemFromReq.Size;
                                    taskItem.Remark = itemFromReq.Note;
                                    taskItem.TaskId = taskId;
                                    taskItemListFroDb.Add(taskItem);
                                }
                            }

                            // 创建任务子条目
                            await projectMeasureTaskItemRepository.AddListAsync(conn, taskItemListFroDb, tran);
                            
                            // 更新主物料状态
                            var materialItem = await projectMaterialRepository.SingleAsync(task.ProjectMaterialItemId);
                            MaterialMachine.Measure(ref materialItem);
                            await projectMaterialRepository.UpdateAsync(conn, materialItem, tran);

                            // 添加log记录
                            var log = new T_ProjectMaterialItemLog();
                            log.CreateTime = DateTime.Now;
                            log.ProjectMaterialItemId = task.ProjectMaterialItemId;
                            log.Remark = "提交测量任务";
                            log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), materialItem.Status);
                            log.Status = materialItem.Status;

                            await logRepository.AddAsync(conn, log, tran);

                            var str = taskItemListFroDb.Aggregate(string.Empty, (current, item) => current + "尺寸：" + item.Size.ToString() + "数量：" + item.Amount + "、");
                            var ma = new MaInfo()
                            {
                                // 行Id，用户知道项目的Id
                                DetailsID = materialItem.DetailsId,
                                // 测量数据
                                MeasureInfo = str
                            };
                            listForU9.Add(ma);
                        }

                        // 更新U9审批流状态回写
                        var client = new JFClient();
                        
                        var response = await client.JFSendProMeasureStatusAsync(CommonConst.U9ServiceSource, timeSign, key, listForU9.ToArray());
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
