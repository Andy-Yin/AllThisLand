namespace Lhs.Entity.ForeignDtos.Response.PlatformMenu
{
    public class ResModulePermission
    {        
        /// <summary>
        /// 模块Id
        /// </summary>
        public int ModuleId { get; set; }
        
        /// <summary>
        /// 父级ModuleId
        /// </summary>
        public int ParentModuleId { get; set; }
        
        /// <summary>
        /// 功能操作-按钮Id
        /// </summary>
        public int PermissionId { get; set; }
        
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        
        /// <summary>
        /// 按钮名称描述
        /// </summary>
        public string PermissionName { get; set; }
    }
}
