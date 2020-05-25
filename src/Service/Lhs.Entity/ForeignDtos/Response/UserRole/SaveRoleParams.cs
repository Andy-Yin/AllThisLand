using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.UserRole
{
    public class SaveRoleParams
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsUsed { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<int> Permission { get; set; }
    }
}