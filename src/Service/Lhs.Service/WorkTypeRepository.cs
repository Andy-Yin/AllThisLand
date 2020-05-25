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
    /// 工种
    /// </summary>
    public class WorkTypeRepository : PlatformBaseService<T_WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取工种列表
        /// </summary>
        public async Task<List<T_WorkType>> GetWorkTypeList()
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT * FROM dbo.T_WorkType WHERE IsDel=0";
                return (await coon.QueryAsync<T_WorkType>(sql)).ToList();
            }
        }

        /// <summary>
        /// 删除工钟
        /// </summary>
        /// <param name="ids">要删除的id </param>
        public async Task<bool> DeleteWorkType(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_WorkType SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
