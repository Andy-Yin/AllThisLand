using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Response.Setting
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperateLog
    {
        /// <summary>
        /// 页码
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 操作开始时间
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 操作类型：后台 app u9 微信
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestParameter { get; set; }
    }
}
