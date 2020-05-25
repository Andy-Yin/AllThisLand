using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.PlatformMenu
{
    /// <summary>
    /// 平台菜单返回实体
    /// </summary>
    public class MenuData
    {
        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 分组id
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<ChildrenMenu> Children { get; set; }
    }
}
