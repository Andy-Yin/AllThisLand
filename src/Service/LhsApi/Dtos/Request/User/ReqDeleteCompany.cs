using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqDeleteCompany : ReqAuth
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Required(ErrorMessage = "CompanyId不能为空")]
        public string CompanyId { get; set; }

    }
}
