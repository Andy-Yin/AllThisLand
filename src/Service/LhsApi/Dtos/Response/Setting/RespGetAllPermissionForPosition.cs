using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.Setting
{
    /// <summary>
    /// 父菜单信息
    /// </summary>
    public class RespGetAllPermissionForPosition
    {
        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuInfo> Children { get; set; } = new List<MenuInfo>();
    }

    /// <summary>
    /// 子菜单信息
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 按钮
        /// </summary>
        public List<ButtonInfo> Children { get; set; } = new List<ButtonInfo>();
    }

    /// <summary>
    /// 按钮信息
    /// </summary>
    public class ButtonInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = string.Empty;


        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
    }
}
