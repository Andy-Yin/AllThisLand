using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.User
{
    public class RespUserInfo
    {
        public RespUserInfo()
        {
        }

        public RespUserInfo(UserInfo user)
        {
            UserId = user.Id;
            UserName = user.Name;
            Sex = user.Sex ? "女" : "男";
            Phone = user.Phone;
            RegionName = user.RegionName;
            CompanyId = user.CompanyId;
            DepartmentId = user.DepartmentId;
            CompanyName = user.CompanyName;
            DepartmentName = user.DepartmentName;
        }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<int> PermissionList { get; set; }
    }
}
