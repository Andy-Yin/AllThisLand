using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;

namespace Lhs.Entity.ForeignDtos.Response.News
{
    public class ResNewsImg
    {
        public int Id { get; set; }

        private string _imgUrl;

        /// <summary>
        /// Banner地址
        /// </summary>
        public string ImgUrl
        {
            get => PicHelper.ConcatPicUrl(_imgUrl);
            set => _imgUrl = value;
        }

        /// <summary>
        /// 图片的跳转地址
        /// </summary>
        public string OpenUrl { get; set; }

    }
}
