using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhs.Entity.ForeignDtos.Response
{
    /// <summary>
    /// 获取机构id
    /// </summary>
    public class GetTrainingInstitutionId
    {
        /// <summary>
        /// 视频水印图片地址
        /// </summary>
        public string VideoWatermarking { get; set; }

        /// <summary>
        /// 机构id 
        /// </summary>
        public int TrainingInstitutionId { set; get; }

        /// <summary>
        /// 标志位：0.正常，1.版本过期，2.增值服务未缴费，不可使用增值服务
        /// </summary>
        public int Mark { set; get; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string TrainingName { set; get; }

        /// <summary>
        /// Seo关键字
        /// </summary>
        public string SeoKeyWords { set; get; }

        /// <summary>
        /// Seo关键字描述
        /// </summary>
        public string SeoDescribe { set; get; }

        /// <summary>
        /// APP下载二维码
        /// </summary>
        public string QrcodeUrl { set; get; }

        /// <summary>
        /// Footer名称
        /// </summary>
        public string FooterName { set; get; }

        /// <summary>
        /// Footer链接
        /// </summary>
        public string FooterUrl { set; get; }

        /// <summary>
        /// 用户协议名称
        /// </summary>
        public string ProtocolName { set; get; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactPhone { set; get; } = string.Empty;

        /// <summary>
        /// 机构地址
        /// </summary>
        public string InstitutionAddress { set; get; } = string.Empty;

        /// <summary>
        /// 学校Logo
        /// </summary>
        public string SchoolLogo { set; get; }

        /// <summary>
        /// 学校 H5 Logo
        /// </summary>
        public string SchoolH5Logo { get; set; }

        /// <summary>
        /// 跑马灯配置：true开启 false关闭
        /// </summary>
        public bool DanmakuFlag { set; get; }

        /// <summary>
        /// 机构样式 0：菜单式 1：矩阵式
        /// </summary>
        public int MenueStyle { set; get; }

        public List<OfficialInfo> OfficialList = new List<OfficialInfo>();

        /// <summary>
        /// 客服在线Url
        /// </summary>
        public string OnlineServiceUrl { set; get; } = string.Empty;

        /// <summary>
        /// 经度
        /// </summary>
        public double Lng { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 机构公开试卷可试做题数（-1为不限制）
        /// </summary>
        public int TryQuestionCount { get; set; }

        /// <summary>
        /// 微信appId
        /// </summary>
        public string WxAppId { get; set; }

        /// <summary>
        /// 机构奖学金
        /// </summary>
        public int Scholarship { get; set; }

        /// <summary>
        /// 机构行业列表
        /// </summary>
        public List<string> Industry { get; set; } = new List<string>();

        /// <summary>
        /// 机构行业下分类id名称
        /// </summary>
        public List<TrainingInstitutionCategoryInfo> CategoryList { get; set; } = new List<TrainingInstitutionCategoryInfo>();
    }

    /// <summary>
    /// 公众号信息
    /// </summary>
    public class OfficialInfo
    {
        /// <summary>
        /// 类型：1：微信公众号 2：微博公众号
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 二维码图片
        /// </summary>
        public string Img { set; get; } = string.Empty;

        /// <summary>
        /// 对应名称
        /// </summary>
        public string Name { set; get; } = string.Empty;
    }

    /// <summary>
    /// 机构分类信息
    /// </summary>
    public class TrainingInstitutionCategoryInfo
    {
        public int IndustryId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
