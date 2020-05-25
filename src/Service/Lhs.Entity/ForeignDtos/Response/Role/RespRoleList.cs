namespace Lhs.Entity.ForeignDtos.Response.Role
{
    public class RespRoleList
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int Id { get; set; }

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
        public bool IsUsed { get; set; }
    }
}