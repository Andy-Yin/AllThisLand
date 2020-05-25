using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Project;

namespace Lhs.Service
{
    /// <summary>
    /// 表T_ProjectConstructionPlan的Service
    /// </summary>
    public class ProjectConstructionPlanRepository : PlatformBaseService<T_ProjectConstructionPlan>, IProjectConstructionPlanRepository
    {
        public ProjectConstructionPlanRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_ProjectConstructionPlan>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ProjectConstructionPlan WHERE IsDel=0";
                return (await coon.QueryAsync<T_ProjectConstructionPlan>(sql)).ToList();
            }
        }

        /// <summary>
        /// 获取项目下的施工计划基础数据
        /// </summary>
        public async Task<List<T_ProjectConstructionPlan>> GetProjectConstructionPlanList(int projectId)
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT * FROM dbo.T_ProjectConstructionPlan WHERE IsDel=0 AND ProjectId=@projectId AND Status<3";
                return (await coon.QueryAsync<T_ProjectConstructionPlan>(sql, new { projectId })).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectConstructionPlan SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }

        /// <summary>
        /// 获取项目下的施工计划
        /// </summary>
        public async Task<List<ProjectPlanInfo>> GetProjectPlans(int projectId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    WITH FlowRecord
                    AS (SELECT *
                        FROM dbo.T_ProjectFlowRecord record WITH (NOLOCK)
                        WHERE record.ProjectId = @projectId
                              AND record.Result IN ( 1, 2 ))
                    SELECT b.Type,
                           b.PlanName,
                           b.StartTime,
                           b.EndTime
                    FROM
                    (
                        --审批流的实际时间
                        SELECT a.Type,
                               '' AS PlanName,
                               (
                                   SELECT MIN(record2.CreateTime)
                                   FROM FlowRecord record2
                                   WHERE record2.Type = a.Type
                                         AND record2.FlowNodeId = a.StartNode
                               ) AS StartTime,
                               (
                                   SELECT MAX(record2.CreateTime)
                                   FROM FlowRecord record2
                                   WHERE record2.Type = a.Type
                                         AND record2.FlowNodeId = a.EndNode
                               ) AS EndTime,
                               0 AS num
                        FROM
                        (
                            SELECT flow.Type,
                                   flow.Id AS StartNode,
                                   (
                                       SELECT flow2.Id
                                       FROM dbo.T_FlowNode flow2 WITH (NOLOCK)
                                       WHERE flow2.Type = flow.Type
                                             AND
                                             (
                                                 flow2.NextNodeId = 0
                                                 OR
                                                 (
                                                     SELECT flow3.PreNodeId
                                                     FROM dbo.T_FlowNode flow3 WITH (NOLOCK)
                                                     WHERE flow3.Id = flow2.NextNodeId
                                                 ) = 0
                                             )
                                   ) AS EndNode
                            FROM dbo.T_FlowNode flow WITH (NOLOCK)
                            WHERE flow.PreNodeId = 0
                                  AND flow.Type IN ( 1, 2, 3, 4 )
                        ) a
                        UNION
                        --施工管理的实际时间
                        SELECT 5 AS Type,
                               pManage.CategoryName AS PlanName,
                               MIN(pw.CreateTime) AS StartTime,
                               CASE
                                   WHEN NOT EXISTS
                                            (
                                                SELECT task.Id
                                                FROM dbo.T_ProjectConstructionCheckTask task WITH (NOLOCK)
                                                WHERE task.ProjectManageId = pManage.Id
                                                      AND task.IsDel = 0
                                                      AND
                                                      (
                                                          task.Status IS NULL
                                                          OR task.Status <> 2
                                                      )
                                            ) THEN
                                   (
                                       SELECT MAX(task.EditTime)
                                       FROM dbo.T_ProjectConstructionCheckTask task WITH (NOLOCK)
                                       WHERE task.ProjectManageId = pManage.Id
                                             AND task.IsDel = 0
                                   )
                                   ELSE
                                       NULL
                               END AS EndTime,
                               ROW_NUMBER() OVER (ORDER BY pManage.Id) AS num
                        FROM dbo.T_ProjectConstructionManage pManage WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectWorker pw WITH (NOLOCK)
                                ON pManage.Id = pw.ProjectManageId
                                   AND pw.IsDel = 0
                        WHERE pManage.ProjectId = @projectId
                              AND pManage.IsDel = 0
                        GROUP BY pManage.Id,
                                 pManage.CategoryName
                        UNION
                        --测量任务的实际时间
                        SELECT 6 AS Type,
                               m.SecondCategoryName AS PlanName,
                               MIN(task.CreateTime) AS StartTime, --第一个任务的创建时间作为开始时间
                               CASE
                                   WHEN SUM(task.Status) / COUNT(task.Status) = 3 THEN
                                       MAX(task.EditTime) --所有任务都完成时，最大的编辑时间作为结束时间
                                   ELSE
                                       NULL
                               END AS EndTime,
                               ROW_NUMBER() OVER (ORDER BY m.CategoryId, m.SecondCategoryId) AS num
                        FROM dbo.T_ProjectMaterialItem m WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectMeasureTask task WITH (NOLOCK)
                                ON m.Id = task.ProjectMaterialItemId
                                   AND task.IsDel = 0
                        WHERE m.ProjectId = @projectId
                              AND m.IsDel = 0
                              AND m.NeedMeasure = 1 --需要测量
                        GROUP BY m.CategoryId,
                                 m.SecondCategoryId,
                                 m.SecondCategoryName
                        UNION
                        --下单任务的实际时间
                        SELECT 7 AS Type,
                               m.SecondCategoryName AS PlanName,
                               MIN(task.CreateTime) AS StartTime, --第一个任务的创建时间作为开始时间
                               CASE
                                   WHEN SUM(task.Status) / COUNT(task.Status) = 3 THEN
                                       MAX(task.EditTime) --所有任务都完成时，最大的编辑时间作为结束时间
                                   ELSE
                                       NULL
                               END AS EndTime,
                               ROW_NUMBER() OVER (ORDER BY m.CategoryId, m.SecondCategoryId) AS num
                        FROM dbo.T_ProjectMaterialItem m WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectOrderTask task WITH (NOLOCK)
                                ON m.Id = task.ProjectMaterialItemId
                                   AND task.IsDel = 0
                        WHERE m.ProjectId = @projectId
                              AND m.IsDel = 0
                              AND m.NeedOrder = 1 --需要下单
                        GROUP BY m.CategoryId,
                                 m.SecondCategoryId,
                                 m.SecondCategoryName
                        UNION
                        --安装任务的实际时间
                        SELECT 8 AS Type,
                               m.SecondCategoryName AS PlanName,
                               MIN(task.CreateTime) AS StartTime, --第一个任务的创建时间作为开始时间
                               CASE
                                   WHEN SUM(task.Status) / COUNT(task.Status) = 3 THEN
                                       MAX(task.EditTime) --所有任务都完成时，最大的编辑时间作为结束时间
                                   ELSE
                                       NULL
                               END AS EndTime,
                               ROW_NUMBER() OVER (ORDER BY m.CategoryId, m.SecondCategoryId) AS num
                        FROM dbo.T_ProjectMaterialItem m WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectInstallTask task WITH (NOLOCK)
                                ON m.Id = task.ProjectMaterialItemId
                                   AND task.IsDel = 0
                        WHERE m.ProjectId = @projectId
                              AND m.IsDel = 0
                        GROUP BY m.CategoryId,
                                 m.SecondCategoryId,
                                 m.SecondCategoryName
                    ) b
                    ORDER BY b.Type,
                             b.num;";
                return (await coon.QueryAsync<ProjectPlanInfo>(sql, new { projectId })).ToList();
            }
        }
    }
}
