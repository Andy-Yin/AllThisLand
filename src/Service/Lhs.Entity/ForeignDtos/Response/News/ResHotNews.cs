using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;

namespace Lhs.Entity.ForeignDtos.Response.News
{
    public class ResHotNews
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string Title { get; set; }

        private string _imgUrl;

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl
        {
            get => PicHelper.ConcatPicUrl(_imgUrl);
            set => _imgUrl = value;
        }

    }
}
