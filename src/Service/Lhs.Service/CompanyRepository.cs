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
    /// 分公司
    /// </summary>
    public class CompanyRepository : PlatformBaseService<T_Company>, ICompanyRepository
    {
        public CompanyRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取分公司
        /// </summary>
        public async Task<T_Company> GetCompany(string companyId)
        {
            using (var coon = Connection)
            {
                var querySql = "SELECT * FROM T_Company WHERE CompanyId = @companyId AND IsDel = 0";
                var user = await coon.QueryFirstOrDefaultAsync<T_Company>(querySql, new { companyId });
                return user;
            }
        }

        /// <summary>
        /// 删除分公司
        /// </summary>
        public async Task<bool> DeleteCompany(string companyId)
        {
            using (var coon = Connection)
            {
                var querySql = "UPDATE dbo.T_Company SET IsDel=1,EditTime=GETDATE() WHERE CompanyId=@companyId";
                return await coon.ExecuteAsync(querySql, new { companyId }) > 0;
            }
        }

        /// <summary>
        /// 获取所有分公司
        /// </summary>
        public async Task<List<T_Company>> GetAllCompany()
        {
            using (var coon = Connection)
            {
                var querySql = $"SELECT * FROM T_Company WHERE IsDel = 0";
                return (await coon.QueryAsync<T_Company>(querySql)).ToList();
            }
        }
    }
}
