using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 工人变更
    /// </summary>
    public class WorkerChangeRepository : PlatformBaseService<T_ProjectWorkerChangeRecord>, IWorkerChangeRepository
    {
        public WorkerChangeRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 删除工人变更
        /// </summary>
        /// <param name="ids">要删除的id </param>
        public async Task<bool> DeleteChange(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectWorkerChangeRecord SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }

        /// <summary>
        /// 工人变更
        /// </summary>
        public async Task<bool> ChangeWorker(int projectId, int type, int oldWorkerId, int newWorkerId)
        {
            using (var coon = Connection)
            {
                var sql = string.Empty;
                if (type == (int)WorkerEnum.WorkerChangeType.ConstructionWorker)
                {
                    sql = @";
                    UPDATE dbo.T_ProjectWorker
                    SET WorkerId = @newWorkerId, --新工人
                    EditTime = GETDATE()
                    FROM dbo.T_ProjectWorker pw WITH(NOLOCK)
                    INNER JOIN dbo.T_ProjectConstructionManage pmanage WITH(NOLOCK)
                    ON pw.ProjectManageId = pmanage.Id
                    AND pw.IsDel = 0
                    AND pmanage.IsDel = 0
                    AND pmanage.ProjectId = @projectId--当前项目
                        AND pw.WorkerId = @oldWorkerId; --原工人";
                }
                else if (type == (int)WorkerEnum.WorkerChangeType.InstallWorker)
                {
                    sql = @";
                        UPDATE dbo.T_ProjectInstallTask
                        SET WorkerId = w.Id, --新工人
                            WorkerName = w.Name,
                            WorkerPhone = w.Phone,
                            EditTime = GETDATE()
                        FROM dbo.T_ProjectInstallTask task WITH (NOLOCK)
                            INNER JOIN dbo.T_Worker w WITH (NOLOCK)
                                ON task.ProjectId = @projectId --当前项目
                                   AND w.Id = @newWorkerId --新工人
                                   AND task.IsDel = 0
                                   AND task.WorkerId = @oldWorkerId; --原工人";
                }
                else
                {
                    return false;
                }
                var result = await coon.ExecuteAsync(sql, new { projectId, oldWorkerId, newWorkerId });
                return result > 0;
            }
        }
    }
}
