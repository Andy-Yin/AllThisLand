using Core.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Common.Const;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Service
{
    /// <summary>
    /// 派工
    /// </summary>
    public class WorkerRepository : PlatformBaseService<T_Worker>, IWorkerRepository
    {
        public WorkerRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取工人变更列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="type">0 全部 1 施工工人 2 安装工人</param>
        /// <param name="pageNum">第几页</param>
        public async Task<PageResponse<WorkerChangeInfo>> GetAssignWorkerList(int projectId, int type
            , int pageNum, string minDate, string maxDate, string searchKey)
        {
            using (var coon = Connection)
            {
                var where = string.Empty;
                var startDate = DateTime.Now;
                var endDate = DateTime.Now;

                if (projectId > 0)
                {
                    where += " AND record.ProjectId = @projectId";
                }
                else
                {
                    if (type == (int)WorkerEnum.WorkerChangeType.ConstructionWorker ||
                        type == (int)WorkerEnum.WorkerChangeType.InstallWorker)
                    {
                        where += " AND record.Type = @type";
                    }

                    if (!string.IsNullOrEmpty(searchKey))
                    {
                        where += @"
                          AND
                          (
                              record.ChangeNo LIKE @searchKey
                              OR project.ProjectName LIKE @searchKey
                              OR (SELECT TOP 1 Name FROM dbo.T_User WHERE U9UserId=project.ConstructionMasterId) LIKE @searchKey
                          )";
                    }
                    if (!string.IsNullOrEmpty(minDate))
                    {
                        startDate = DateTime.Parse(minDate).Date;
                        where += @" AND record.CreateTime > @startDate";
                    }
                    if (!string.IsNullOrEmpty(maxDate))
                    {
                        endDate = DateTime.Parse(maxDate).Date.AddDays(1);
                        where += @" AND record.CreateTime < @endDate";
                    }
                }

                var sql = $@"
                    SELECT record.*,
                           project.ProjectName,
                           (
                               SELECT Name
                               FROM dbo.T_User
                               WHERE U9UserId = project.ConstructionMasterId
                                     AND IsDel = 0
                           ) AS ConstructionMasterName,
                           (
                               SELECT Name
                               FROM dbo.T_User
                               WHERE U9UserId = project.SupervisionId
                                     AND IsDel = 0
                           ) AS SupervisionName,
                           (
                               SELECT wt.Name
                               FROM dbo.T_WorkType wt WITH (NOLOCK)
                               WHERE wt.Id = record.WorkType
                           ) AS WorkTypeName,
                           (
                               SELECT w.Name
                               FROM dbo.T_Worker w WITH (NOLOCK)
                               WHERE w.Id = record.OldWorkerId
                           ) AS OldWorkerName,
                           (
                               SELECT w.Name
                               FROM dbo.T_Worker w WITH (NOLOCK)
                               WHERE w.Id = record.NewWorkerId
                           ) AS NewWorkerName
                    FROM dbo.T_ProjectWorkerChangeRecord record WITH (NOLOCK)
                        INNER JOIN dbo.T_Project project WITH (NOLOCK)
                            ON record.ProjectId = project.Id
                               AND record.IsDel = 0
                    WHERE 1 = 1
                          {where}";
                return await PagedAsync<WorkerChangeInfo>(sql, new
                {
                    projectId,
                    type = type - 1,
                    startDate,
                    endDate,
                    searchKey = $"%{searchKey}%"
                }, "", pageNum,
                    CommonConst.PageSize);

            }
        }

        /// <summary>
        /// 获取工人列表
        /// </summary>
        public async Task<List<WorkerInfo>> GetWorkerList(string companyId, int workType, string searchKey)
        {
            using (var coon = Connection)
            {
                var where = string.Empty;
                if (!string.IsNullOrEmpty(companyId))
                {
                    where += " AND w.CompanyId = @companyId";
                }
                if (workType > 0)
                {
                    where += " AND w.WorkType = @workType";
                }
                if (!string.IsNullOrEmpty(searchKey))
                {
                    where += @"
                          AND
                          (
                              w.Name LIKE @searchKey
                              OR w.Phone LIKE @searchKey
                          )";
                }

                var sql = $@"
                            SELECT w.*,
                                   wt.Name AS WorkTypeName,
                                   c.Name AS CompanyName
                            FROM dbo.T_Worker w WITH (NOLOCK)
                                INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                                    ON w.WorkType = wt.Id
                                       AND w.IsDel = 0
                                       AND wt.IsDel = 0
                                INNER JOIN dbo.T_Company c WITH (NOLOCK)
                                    ON w.CompanyId = c.CompanyId
                            WHERE 1 = 1
                          {where}
                            ORDER BY w.Id";
                return (await coon.QueryAsync<WorkerInfo>(sql, new
                {
                    companyId,
                    workType,
                    searchKey = $"%{searchKey}%"
                })).ToList();

            }
        }

        /// <summary>
        /// 获取工人信息
        /// </summary>
        public async Task<WorkerInfo> GetWorkerInfo(int workerId)
        {
            using (var coon = Connection)
            {
                var sql = $@"
                    SELECT w.*,
                           wt.Name AS WorkTypeName,
                           c.Name AS CompanyName
                    FROM dbo.T_Worker w WITH (NOLOCK)
                        INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                            ON w.WorkType = wt.Id
                               AND w.IsDel = 0
                               AND wt.IsDel = 0
                        INNER JOIN dbo.T_Company c WITH (NOLOCK)
                            ON w.CompanyId = c.CompanyId
                    WHERE w.Id = @workerId;";
                return await coon.QueryFirstOrDefaultAsync<WorkerInfo>(sql, new { workerId });
            }
        }

        /// <summary>
        /// 获取项目的工人列表
        /// </summary>
        public async Task<List<WorkerInfo>> GetWorkerList(int projectId, int type)
        {
            using (var coon = Connection)
            {
                var sql = string.Empty;
                if (type == (int)WorkerEnum.WorkerChangeType.ConstructionWorker)
                {
                    sql = $@"
                        SELECT DISTINCT
                               w.Id,
                               w.Name,
                               w.Phone,
                               w.Sex,
                               w.CompanyId,
                               w.WorkType,
                               wt.Name AS WorkTypeName,
                               c.Name AS CompanyName
                        FROM dbo.T_ProjectWorker pw WITH (NOLOCK)
                            INNER JOIN dbo.T_ProjectConstructionManage pmanage WITH (NOLOCK)
                                ON pw.IsDel = 0
                                   AND pw.ProjectManageId = pmanage.Id
                                   AND pmanage.IsDel = 0
                                   AND pmanage.ProjectId = @projectId --项目id
                            INNER JOIN dbo.T_Worker w WITH (NOLOCK)
                                ON pw.WorkerId = w.Id
                                   AND w.IsDel = 0
                            INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                                ON w.WorkType = wt.Id
                                   AND w.IsDel = 0
                                   AND wt.IsDel = 0
                            INNER JOIN dbo.T_Company c WITH (NOLOCK)
                                ON w.CompanyId = c.CompanyId
                        ORDER BY w.Id;";
                }
                else if (type == (int)WorkerEnum.WorkerChangeType.InstallWorker)
                {
                    sql = $@"
                    SELECT DISTINCT
                           w.Id,
                           w.Name,
                           w.Phone,
                           w.Sex,
                           w.CompanyId,
                           w.WorkType,
                           wt.Name AS WorkTypeName,
                           c.Name AS CompanyName
                    FROM dbo.T_ProjectInstallTask install WITH (NOLOCK)
                        INNER JOIN dbo.T_Worker w WITH (NOLOCK)
                            ON install.WorkerId = w.Id
                               AND install.IsDel = 0
                               AND install.ProjectId = @projectId --项目id
                               AND w.IsDel = 0
                        INNER JOIN dbo.T_WorkType wt WITH (NOLOCK)
                            ON w.WorkType = wt.Id
                               AND w.IsDel = 0
                               AND wt.IsDel = 0
                        INNER JOIN dbo.T_Company c WITH (NOLOCK)
                            ON w.CompanyId = c.CompanyId
                    ORDER BY w.Id;";
                }
                else
                {
                    return new List<WorkerInfo>();
                }
                return (await coon.QueryAsync<WorkerInfo>(sql, new
                {
                    projectId
                })).ToList();
            }
        }

        /// <summary>
        /// 删除工人
        /// </summary>
        /// <param name="ids">要删除的id </param>
        public async Task<bool> DeleteWorker(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_Worker SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
