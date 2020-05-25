using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqUpdateCompany : ReqAuth
    {

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "CompanyId不能为空")]
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; } = string.Empty;

    }
}
