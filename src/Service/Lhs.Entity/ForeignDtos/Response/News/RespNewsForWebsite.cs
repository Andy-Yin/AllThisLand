using System.Collections.Generic;
using Lhs.Common;

namespace Lhs.Entity.ForeignDtos.Response.News
{
    public class RespNewsForWebsite
    {
        /// <summary>
        /// 新闻列表
        /// </summary>
        public List<RespNewsList> NewsList { get; set; }= new List<RespNewsList>();

        /// <summary>
        /// 当前页码
        /// </summary>
        public long CurrentPage;

        /// <summary>
        /// 总条数
        /// </summary>
        public long SumItem;

        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPage;
    }

    public class RespNewsList
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        private string _imgUrl;

        /// <summary>
        /// 封面图片 - 新闻图片或视频展示图
        /// </summary>
        public string ImgUrl
        {
            get => PicHelper.ConcatPicUrl(_imgUrl);
            set => _imgUrl = value;
        }

        /// <summary>
        /// 新闻简介 - 可以是一句话，也可以是一段话，不超过100字，必填项
        /// </summary>
        public string NewsDesc { get; set; }

        /// <summary>
        /// 新闻id
        /// </summary>
        public string NewsId { get; set; }

        /// <summary>
        /// 新闻标注 - 0，无；1：推荐；2：热点；
        /// </summary>
        public int NewsTip { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string NewsTypeName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
    }
}
