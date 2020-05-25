using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.User
{
    public class RespLoginForAPP
    {
        public RespLoginForAPP()
        {
        }

        public RespLoginForAPP(UserInfo userInfo)
        {
            UserId = userInfo.Id;
            UserName = userInfo.Name;
            if (!string.IsNullOrEmpty(userInfo.CompanyId) && !string.IsNullOrEmpty(userInfo.CompanyName))
            {
                CompanyList.Add(new CompanyInfo
                {
                    CompanyId = userInfo.CompanyId ?? string.Empty,
                    CompanyName = userInfo.CompanyName ?? string.Empty
                });
            }
        }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public List<CompanyInfo> CompanyList { get; set; } = new List<CompanyInfo>();

    }

    public class CompanyInfo
    {
        public string CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
