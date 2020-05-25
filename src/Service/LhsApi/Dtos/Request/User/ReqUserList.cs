using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.User
{
    public class ReqUserList : ReqBasePage
    {
        /// <summary>
        /// 搜过关键字：用户姓名，用户手机号
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 大区名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 大区id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 岗位id
        /// </summary>
        public int PositionId { get; set; }
    }
}
