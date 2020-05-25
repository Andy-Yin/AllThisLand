using System.ComponentModel.DataAnnotations;
using Lhs.Entity.ForeignDtos.Request;

namespace LhsAPI.Dtos.Request.Common
{
    public class ReqUploadImg : ReqAuth
    {
        /// <summary>
        /// 图片（base64格式）
        /// </summary>
        [Required(ErrorMessage = "参数不能为空")]
        public string ImgEncode { set; get; }

        /// <summary>
        /// 类型：不同类型存在不同文件夹下
        /// 0 公共 1 施工管理验收记录 2 跟进日志 3 质检管理 4 签收
        /// </summary>
        public int Type { set; get; }
    }
}
