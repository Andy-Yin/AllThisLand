using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;

namespace Lhs.Entity.ForeignDtos.Response.News
{
    public class ResNews
    {
        public int Id { get; set; }

        /// <summary>
        /// 资讯类型Id
        /// </summary>
        public int NewsTypeId { get; set; }

        /// <summary>
        /// 资讯标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        private string _imgUrl;

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ImgUrl
        {
            get => PicHelper.ConcatPicUrl(_imgUrl);
            set => _imgUrl = value;
        }

        /// <summary>
        /// 简要
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 点击量/浏览量
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 状态 0：已发布 1：待发布 2：已下架
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 标注 0：无 1：推荐 2：热点
        /// </summary>
        public int NewsTip { get; set; }

        /// <summary>
        /// 资讯类型名称
        /// </summary>
        public string NewsTypeName { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间的字符串
        /// </summary>
        public string CreateTimeStr { get; set; }
    }
}
