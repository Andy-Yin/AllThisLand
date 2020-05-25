using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqUpdateUser : ReqAuth
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "用户id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Phone { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 性别：0 男 1 女，默认男
        /// </summary>
        public bool Sex { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentId { get; set; } = string.Empty;

    }
}
