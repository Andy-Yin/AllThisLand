using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqEnableOrDisableUser : ReqAuth
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// false 禁用 true启用
        /// </summary>
        public bool IsUsed { get; set; }

    }
}
