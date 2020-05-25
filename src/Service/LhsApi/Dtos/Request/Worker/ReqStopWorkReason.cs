using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 保存停工原因
    /// </summary>
    public class ReqStopWorkReason
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; set; }

    }
}
