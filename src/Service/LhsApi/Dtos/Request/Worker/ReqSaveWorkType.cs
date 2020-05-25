using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 保存工种
    /// </summary>
    public class ReqSaveWorkType : ReqAuth
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int WorkTypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string WorkTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Desc { get; set; }

    }
}
