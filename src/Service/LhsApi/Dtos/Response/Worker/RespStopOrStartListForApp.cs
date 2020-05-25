using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Response.Worker
{
    public class RespStopOrStartListForApp
    {
        public int RecordId { get; set; }

        /// <summary>
        /// 类型：停工申请，还是复工申请
        /// </summary>
        public string TypeName { get; set; }

        public int Status { get; set; }

        //停工时间-只有停工使用
        public int StopDays { get; set; } = 0;

        /// <summary>
        /// 停工原因-只有停工使用
        /// </summary>
        public string StopReason { get; set; } = "";

        /// <summary>
        /// 计划停工或者复工日期
        /// </summary>
        public string PlanDate { get; set; }
    }
}
