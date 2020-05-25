using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Project
{
    /// <summary>
    /// 客户的项目
    /// </summary>
    public class ReqCustomerProject : ReqAuth
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
    }
}
