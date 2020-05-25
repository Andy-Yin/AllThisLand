using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Project
{
    /// <summary>
    /// 获取项目匹配的模板
    /// </summary>
    public class ReqGetMatchingTemplate : ReqAuth
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
