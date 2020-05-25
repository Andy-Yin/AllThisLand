using Core.Data;
using Core.Util.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Entity.ForeignDtos.Request.User;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Dapper.Contrib.Extensions;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Service
{
    /// <summary>
    /// 施工管理
    /// </summary>
    public class ConstructionRepository : PlatformBaseService<T_ProjectConstructionManage>, IConstructionRepository
    {
        public ConstructionRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：3 施工管理 4 施工计划</param>
        public async Task<bool> DeleteTemplate(List<int> ids, int type)
        {
            using (var coon = Connection)
            {
                var tableName = EnumHelper.GetDescription(typeof(ConstructionEnum.ConstructionTemplateType), type);
                var sql = $"UPDATE dbo.{tableName} SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql, new { type = type - 1 });
                return result > 0;
            }
        }

        /// <summary>
        /// 获取施工列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        public async Task<List<T_ProjectConstructionManage>> GetConstructionList(int projectId)
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ProjectConstructionManage WITH(NOLOCK) WHERE ProjectId=@projectId AND IsDel=0 ORDER BY CategoryId";
                return (await coon.QueryAsync<T_ProjectConstructionManage>(sql, new { projectId })).ToList();
            }
        }

        /// <summary>
        /// 获取派工列表
        /// </summary>
        /// <param name="workType">后台施工管理里的分类/工种</param>
        /// <param name="pageNum">第几页</param>
        public async Task<PageResponse<WorkerInfo>> GetAssignWorkerList(int workType, int pageNum)
        {
            using (var coon = Connection)
            {
                var sql = @"
                    SELECT w.*,
                           pw.Id AS AssignId,
                           pw.CreateTime AS AssignTime,
                           c.Name AS CompanyName,
                           wt.Name AS WorkTypeName
                    FROM dbo.T_ProjectWorker pw WITH (NOLOCK)
                        INNER JOIN dbo.T_Worker w WITH (NOLOCK)
                            ON pw.WorkerId = w.Id
                               AND w.IsDel = 0
                               AND pw.IsDel = 0
                               AND pw.ProjectManageId = @workType
                        INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                            ON w.WorkType = wt.Id
                        LEFT JOIN dbo.T_Company c WITH (NOLOCK)
                            ON w.CompanyId = c.CompanyId";
                return await PagedAsync<WorkerInfo>(sql, new { workType }, "AssignTime", pageNum, CommonConst.PageSize);
            }
        }

        /// <summary>
        /// 获取验收列表
        /// </summary>
        /// <param name="workType">后台施工管理里的分类/工种</param>
        /// <param name="pageNum">第几页</param>
        public async Task<PageResponse<CheckTask>> GetCheckList(int workType, int pageNum)
        {
            using (var coon = Connection)
            {
                var sql = @"
                    SELECT ptask.*,
                           task.Name AS TaskName,
                           (
                               SELECT TOP 1
                                      CONCAT(wt.Name, '-', w.Name)
                               FROM dbo.T_ProjectWorker pw WITH (NOLOCK)
                                   INNER JOIN dbo.T_Worker w WITH (NOLOCK)
                                       ON pw.WorkerId = w.Id
                                          AND pw.IsDel = 0
                                          AND w.IsDel = 0
                                          AND pw.ProjectManageId = @workType
                                   INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                                       ON w.WorkType = wt.Id
                                          AND wt.IsDel = 0
                           ) AS WorkerName,
                           (
                               SELECT TOP 1
                                      w.Phone
                               FROM dbo.T_ProjectWorker pw WITH (NOLOCK)
                                   INNER JOIN dbo.T_Worker w WITH (NOLOCK)
                                       ON pw.WorkerId = w.Id
                                          AND pw.IsDel = 0
                                          AND w.IsDel = 0
                                          AND pw.ProjectManageId = @workType
                                   INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                                       ON w.WorkType = wt.Id
                                          AND wt.IsDel = 0
                           ) AS WorkerPhone
                    FROM dbo.T_ProjectConstructionCheckTask ptask WITH (NOLOCK)
                        INNER JOIN dbo.T_ConstructionManageCheckTask task WITH (NOLOCK)
                            ON ptask.ManageTaskId = task.Id
                               AND task.IsDel = 0
                               AND ptask.IsDel = 0
                               AND ptask.ManageTaskId = @workType";
                return await PagedAsync<CheckTask>(sql, new { workType }, "", pageNum, CommonConst.PageSize);
            }
        }

        /// <summary>
        /// 获取某个验收任务信息
        /// </summary>
        /// <param name="projectTaskId">机构验收任务id</param>
        public async Task<T_ProjectConstructionCheckTask> GetCheckTask(int projectTaskId)
        {
            using (var coon = Connection)
            {
                var sql = @";SELECT * FROM dbo.T_ProjectConstructionCheckTask WITH (NOLOCK) WHERE Id=@projectTaskId AND IsDel=0";
                return await coon.QueryFirstAsync<T_ProjectConstructionCheckTask>(sql, new { projectTaskId });
            }
        }

        /// <summary>
        /// 获取某个验收任务的验收记录
        /// </summary>
        /// <param name="projectTaskId">机构验收任务id</param>
        public async Task<List<CheckRecordInfo>> GetCheckRecordList(int projectTaskId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    SELECT *,
                           (
                               SELECT TOP 1
                                      p.Name
                               FROM dbo.T_UserPosition up WITH (NOLOCK)
                                   INNER JOIN dbo.T_Position p WITH (NOLOCK)
                                       ON up.UserId = record.UserId
                                          AND up.IsDel = 0
                                          AND p.IsDel = 0
                                          AND up.PositionId = p.Id
                           ) AS PositionName
                    FROM dbo.T_ProjectConstructionCheckRecord record WITH (NOLOCK)
                    WHERE record.ProjectTaskId = @projectTaskId;";
                return (await coon.QueryAsync<CheckRecordInfo>(sql, new { projectTaskId })).ToList();
            }
        }

        /// <summary>
        /// 更新验收任务的状态
        /// </summary>
        /// <param name="projectTaskId">机构验收任务id</param>
        public async Task<bool> SaveCheckTaskStatus(int projectTaskId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    --任务状态为已完成
                    UPDATE dbo.T_ProjectConstructionCheckTask
                    SET Status = 2,
                        EditTime = GETDATE()
                    WHERE Id = @projectTaskId;
                    --施工管理对应类目为已完成
                    UPDATE dbo.T_ProjectConstructionManage
                    SET CheckStatus = 2,
                        EditTime = GETDATE()
                    WHERE Id =
                    (
                        SELECT ProjectManageId
                        FROM dbo.T_ProjectConstructionCheckTask
                        WHERE Id = @projectTaskId
                    );";
                return await coon.ExecuteAsync(sql, new { projectTaskId }) > 0;
            }
        }

        /// <summary>
        /// 获取地采模板下的基础数据
        /// </summary>
        public async Task<List<LocalBasicItem>> GetLocalTemplateItemList(int templateId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    SELECT item.*,
                           ISNULL(titem.Id, 0) AS TemplateId
                    FROM dbo.T_LocalMaterialItem item WITH (NOLOCK)
                        LEFT JOIN dbo.T_LocalMaterialTemplateItem titem WITH (NOLOCK)
                            ON item.Id = titem.ItemId
                               AND titem.IsDel = 0
                               AND titem.TemplateId = 1
                    WHERE item.IsDel = 0;";
                return (await coon.QueryAsync<LocalBasicItem>(sql, new { templateId })).ToList();
            }
        }

        /// <summary>
        /// 获取地采基础数据
        /// </summary>
        public async Task<List<LocalBasicItem>> GetLocalBasicItemList(string name)
        {
            using (var coon = Connection)
            {
                string sql;
                if (string.IsNullOrEmpty(name))
                {
                    sql = "SELECT * FROM dbo.T_LocalMaterialItem WITH (NOLOCK) WHERE IsDel=0";
                    return (await coon.QueryAsync<LocalBasicItem>(sql)).ToList();
                }
                sql = "SELECT * FROM dbo.T_LocalMaterialItem WITH (NOLOCK) WHERE  IsDel=0 AND Name LIKE @name";
                return (await coon.QueryAsync<LocalBasicItem>(sql, new { name = $"%{name}%" })).ToList();
            }
        }
    }
}
