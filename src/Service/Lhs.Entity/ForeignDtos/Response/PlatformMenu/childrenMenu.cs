using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.PlatformMenu
{
    /// <summary>
    /// 子菜单
    /// </summary>
    public class ChildrenMenu
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 下级菜单
        /// </summary>
        public List<ChildrenMenu> Children { get; set; }
    }
}
