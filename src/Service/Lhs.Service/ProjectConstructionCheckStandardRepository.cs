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
    /// 表T_ProjectConstructionCheckStandard的Service
    /// </summary>
    public class ProjectConstructionCheckStandardRepository : PlatformBaseService<T_ProjectConstructionCheckStandard>, IProjectConstructionCheckStandardRepository
    {
        public ProjectConstructionCheckStandardRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_ProjectConstructionCheckStandard>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ProjectConstructionCheckStandard WHERE IsDel=0";
                return (await coon.QueryAsync<T_ProjectConstructionCheckStandard>(sql)).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectConstructionCheckStandard SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
