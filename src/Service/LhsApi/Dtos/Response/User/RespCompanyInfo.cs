using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.User
{
    public class RespCompanyInfo
    {
        public RespCompanyInfo()
        {
        }

        public RespCompanyInfo(T_Company company)
        {
            CompanyId = company.CompanyId;
            CompanyName = company.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;
    }
}
