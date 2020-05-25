namespace Lhs.Entity.ForeignDtos.Response.PlatformMenu
{
    public class MenuModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父菜单的id
        /// </summary>
        public int ParentId;

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
    }
}
