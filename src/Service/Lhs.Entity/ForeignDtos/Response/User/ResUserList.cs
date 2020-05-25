using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Common;

namespace Lhs.Entity.ForeignDtos.Response
{
    public class ResUserList
    {
        /// <summary>
        /// UserId
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 用户角色Ids
        /// </summary>
        public string RoleIds { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string RoleNames { get; set; }

        private string _imgUrl;

        /// <summary>
        /// 头像图片
        /// </summary>
        public string HeadImg
        {
            get => PicHelper.ConcatPicUrl(_imgUrl);
            set => _imgUrl = value;
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// 是否选中“全部数据权限"复选框
        /// </summary>
        public bool HasAllPermission { get; set; }
    }
}
