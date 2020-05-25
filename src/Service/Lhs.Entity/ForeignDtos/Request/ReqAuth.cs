using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhs.Entity.ForeignDtos.Request
{
    /// <summary>
    /// 以后废弃，参数不要穿透到底层
    /// 鉴权参数
    /// </summary>
    public class ReqAuth
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeSign { get; set; }

        /// <summary>
        /// 签名值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 操作者Id，如果不需要则为0
        /// </summary>
        public int OperatorId { get; set; } = 0;
    }
}
