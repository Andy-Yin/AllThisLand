using System.ComponentModel.DataAnnotations;

namespace LhsAPI.Dtos.Request.Auth
{
    public class ReqLoginForPlatform
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名"),MaxLength(20,ErrorMessage = "用户名最多20个字")]
        public string Username { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { set; get; }

        ///// <summary>
        ///// 验证码
        ///// </summary>
        //[Required(ErrorMessage = "验证码不能为空")]
        //public string Ticket { get; set; }

        ///// <summary>
        ///// 验证码的随机字符串
        ///// </summary>
        //public string RandStr { get; set; } = "";
    }
}
