using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhs.Entity.ForeignDtos.Response
{
    /// <summary>
    /// 直播客户端的版本信息
    /// </summary>
    public class LiveClientVersion
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 版本说明
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }
    }
}
