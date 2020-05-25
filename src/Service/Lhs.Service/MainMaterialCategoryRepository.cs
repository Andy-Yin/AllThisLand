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
    /// 主材基础数据
    /// </summary>
    public class MainMaterialCategoryRepository : PlatformBaseService<T_MainMaterialCategory>, IMainMaterialCategoryRepository
    {
        public MainMaterialCategoryRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_MainMaterialCategory>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_MainMaterialCategory WHERE IsDel=0";
                return (await coon.QueryAsync<T_MainMaterialCategory>(sql)).ToList();
            }
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_MainMaterialCategory SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
