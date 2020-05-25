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
    /// 表T_Customer的Service
    /// </summary>
    public class CustomerRepository : PlatformBaseService<T_Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_Customer>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_Customer WHERE IsDel=0";
                return (await coon.QueryAsync<T_Customer>(sql)).ToList();
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public async Task<T_Customer> GetCustomerInfo(string phone)
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_Customer WITH(NOLOCK) WHERE Phone=@phone AND IsDel=0";
                return await coon.QueryFirstOrDefaultAsync<T_Customer>(sql, new { phone });
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_Customer SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
