using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request
{
    /// <summary>
    /// 分页请求的基类
    /// </summary>
    public class ReqBasePage
    {
        /// <summary>
        /// 页码 
        /// </summary>
        public int PageNum { get; set; } = 1;

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeSign { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        public string Key { get; set; }
    }
}
