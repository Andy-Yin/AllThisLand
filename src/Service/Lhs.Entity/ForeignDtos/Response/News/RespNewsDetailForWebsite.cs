namespace Lhs.Entity.ForeignDtos.Response.News
{
    public class RespNewsDetailForWebsite
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 新闻类型
        /// </summary>
        public string NewsTypeName { get; set; }

        /// <summary>
        /// 新闻编辑者id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 新闻编辑者名称
        /// </summary>
        public string CreateUserName { get; set; } = string.Empty;

        /// <summary>
        /// 新闻制造时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 音频
        /// </summary>
        public string VideoUrl { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
