using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 用户的基本信息共用类
    /// </summary>
    public class ProjectUser
    {
        public int UserId { get; set; } = 0;

        public string UserName { get; set; } = string.Empty;

        public string UserPhone { get; set; } = string.Empty;

        /// <summary>
        /// 角色
        /// </summary>
        public string RoleName { get; set; } = string.Empty;

        /// <summary>
        /// 用户角色和名称的字符串
        /// </summary>
        public string RoleAndName => RoleName + "-" + UserName;

        /// <summary>
        /// 在项目中的角色
        /// 1-工资
        /// 4-监理
        /// </summary>
        public int PositionId { get; set; } = 0;
    }
}
