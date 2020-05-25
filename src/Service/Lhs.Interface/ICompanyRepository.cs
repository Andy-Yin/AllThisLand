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
    public interface ICompanyRepository : IPlatformBaseService<T_Company>
    {
        /// <summary>
        /// 获取分公司
        /// </summary>
        Task<T_Company> GetCompany(string companyId);


        /// <summary>
        /// 删除分公司
        /// </summary>
        Task<bool> DeleteCompany(string companyId);

        /// <summary>
        /// 获取所有分公司
        /// </summary>
        Task<List<T_Company>> GetAllCompany();
    }
}
