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
    /// 部门
    /// </summary>
    public class DepartmentRepository : PlatformBaseService<T_Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        public async Task<T_Department> GetDepartment(string departmentId)
        {
            using (var coon = Connection)
            {
                var querySql = "SELECT * FROM T_Department WHERE DepartmentId = @departmentId AND IsDel = 0";
                var data = await coon.QueryFirstOrDefaultAsync<T_Department>(querySql, new { departmentId });
                return data;
            }
        }


        /// <summary>
        /// 删除部门
        /// </summary>
        public async Task<bool> DeleteDepartment(string departmentId)
        {
            using (var coon = Connection)
            {
                var querySql = "UPDATE dbo.T_Department SET IsDel=1,EditTime=GETDATE() WHERE CompanyId=@departmentId";
                return await coon.ExecuteAsync(querySql, new { departmentId }) > 0;
            }
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        public async Task<List<T_Department>> GetAllDepartment()
        {
            using (var coon = Connection)
            {
                var querySql = $"SELECT * FROM T_Department WHERE IsDel = 0";
                return (await coon.QueryAsync<T_Department>(querySql)).ToList();
            }
        }
    }
}
