using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 取消订单任务
    /// </summary>
    public class ReqCancelOrderTask : ReqAuth
    {
        /// <summary>
        /// 订单任务Id
        /// </summary>
        public int TaskId { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        public string CancelReason { get; set; }
    }
}
