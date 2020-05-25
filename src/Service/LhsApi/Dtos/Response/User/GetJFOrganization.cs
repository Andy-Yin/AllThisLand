using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LhsAPI.Dtos.Response.User
{
    public class GetJFOrganization
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 数据正确
        /// </summary>
        public string errmsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Organization> data { get; set; }
    }

    public class Organization
    {
        /// <summary>
        /// 
        /// </summary>
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_EnCode { get; set; }
        /// <summary>
        /// 家居科技
        /// </summary>
        public string F_FullName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_EnabledMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string F_Address { get; set; }
    }
}
