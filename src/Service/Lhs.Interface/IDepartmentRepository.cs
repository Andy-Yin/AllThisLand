using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Interface
{
    public interface IDepartmentRepository : IPlatformBaseService<T_Department>
    {
        /// <summary>
        /// 获取部门
        /// </summary>
        Task<T_Department> GetDepartment(string companyId);

        /// <summary>
        /// 删除部门
        /// </summary>
        Task<bool> DeleteDepartment(string departmentId);

        /// <summary>
        /// 获取所有部门
        /// </summary>
        Task<List<T_Department>> GetAllDepartment();
    }
}
