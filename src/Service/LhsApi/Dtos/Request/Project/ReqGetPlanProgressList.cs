using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Project
{
    /// <summary>
    /// 项目的施工计划
    /// </summary>
    public class ReqGetPlanProgressList : ReqAuth
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
