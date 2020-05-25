using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 表T_ProjectPlanStage的Service
    /// </summary>
    public class ProjectPlanStageRepository : PlatformBaseService<T_ProjectPlanStage>, IProjectPlanStageRepository
    {
        public ProjectPlanStageRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_ProjectPlanStage>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ProjectPlanStage WHERE IsDel=0";
                return (await coon.QueryAsync<T_ProjectPlanStage>(sql)).ToList();
            }
        }

        /// <summary>
        /// 获取项目下的施工计划阶段
        /// </summary>
        public async Task<List<T_ProjectPlanStage>> GetProjectPlanStageList(int projectId)
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT * FROM dbo.T_ProjectPlanStage WHERE IsDel=0 AND ProjectId=@projectId";
                return (await coon.QueryAsync<T_ProjectPlanStage>(sql, new { projectId })).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectPlanStage SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
