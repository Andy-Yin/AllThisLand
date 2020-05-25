using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 申请停工
    /// </summary>
    public class ReqStopWork
    {
        public int ProjectId { get; set; }

        /// <summary>
        /// 计划停工天数
        /// </summary>
        public int StopDays { get; set; }

        /// <summary>
        /// 计划停工日期
        /// </summary>

        public DateTime PlanDate { get; set; }

        public int ReasonType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
