namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 菜单权限关系的model
    /// </summary>
    public class ModulePermissionModel
    {
        /// <summary>
        /// 菜单的parentId
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 菜单的id
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 按钮的id
        /// </summary>
        public int PermissionId { get; set; }

        /// <summary>
        /// 权限关系树
        /// </summary>
        public string PermissionStr
        {
            get
            {
                var value = string.Empty;
                if (ParentId == 0)
                {
                    value = $"0-{ModuleId}";
                }
                if (ParentId != 0 && PermissionId == 0)
                {
                    value = $"0-{ParentId}-{ModuleId}";
                }
                if (PermissionId != 0)
                {
                    value = $"0-{ParentId}-{ModuleId}-{PermissionId}";
                }
                return value;
            }

        }
    }
}
