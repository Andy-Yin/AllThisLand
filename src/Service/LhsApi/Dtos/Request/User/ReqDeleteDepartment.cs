using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqDeleteDepartment : ReqAuth
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "DepartmentId不能为空")]
        public string DepartmentId { get; set; }

    }
}
