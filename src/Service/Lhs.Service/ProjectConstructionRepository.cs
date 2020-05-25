using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;

namespace Lhs.Service
{
    /// <summary>
    /// 项目施工管理
    /// </summary>
    public class ProjectConstructionRepository : PlatformBaseService<T_ProjectConstructionManage>, IProjectConstructionRepository
    {
        public ProjectConstructionRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_ProjectConstructionManage>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ProjectConstructionManage WHERE IsDel=0";
                return (await coon.QueryAsync<T_ProjectConstructionManage>(sql)).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectConstructionManage SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }

        /// <summary>
        /// 获取项目下的施工管理基础数据
        /// </summary>
        public async Task<List<ProjectConstructionManage>> GetProjectConstructionManageList(int projectId)
        {
            using (var coon = Connection)
            {
                var sql = @"
                    SELECT pcategory.Id AS CategoryId,
                           pcategory.CategoryName,
                           ptask.Id AS TaskId,
                           ptask.ManageTaskName AS TaskName
                    FROM dbo.T_ProjectConstructionManage pcategory WITH (NOLOCK)
                        LEFT JOIN dbo.T_ProjectConstructionCheckTask ptask WITH (NOLOCK)
                            ON ptask.IsDel = 0
                               AND pcategory.Id = ptask.ProjectManageId
                    WHERE pcategory.IsDel = 0
                          AND pcategory.ProjectId = @projectId
                    ORDER BY pcategory.Id,
                             ptask.Id;";
                return (await coon.QueryAsync<ProjectConstructionManage>(sql, new { projectId })).ToList();
            }
        }
    }
}
