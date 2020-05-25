using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqUpdateDepartment : ReqAuth
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "DepartmentId不能为空")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Name不能为空")]
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }

    }
}
