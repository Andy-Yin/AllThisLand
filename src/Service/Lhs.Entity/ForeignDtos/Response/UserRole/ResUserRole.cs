using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.UserRole
{
    public class ResUserRole
    {
        /// <summary>
        /// 已选角色id的集合
        /// </summary>
        public List<int> SelectedRoles { get; set; } = new List<int>();
        
        /// <summary>
        /// 所有角色
        /// </summary>
        public List<ResAllRole> AllRoles { get; set; } = new List<ResAllRole>();
    }
    
    public class ResAllRole
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}