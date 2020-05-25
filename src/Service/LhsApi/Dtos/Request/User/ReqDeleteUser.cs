using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqDeleteUser: ReqAuth
    {
        /// <summary>
        /// 用户uid
        /// </summary>
        [Required(ErrorMessage = "用户uid不能为空")]
        public string UserId { get; set; }

    }
}
