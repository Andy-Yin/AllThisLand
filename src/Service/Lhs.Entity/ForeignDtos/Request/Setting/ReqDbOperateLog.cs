using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Setting
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class ReqDbOperateLog
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 操作开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 操作结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 操作类型：后台 app u9 微信
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
    }
}
