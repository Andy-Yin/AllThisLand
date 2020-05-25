using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lhs.Entity.ForeignDtos.Response.U9
{
    public class JFSendProStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public int errcode { get; set; }
        /// <summary>
        /// 数据正确
        /// </summary>
        public string errmsg { get; set; }
    }
}
