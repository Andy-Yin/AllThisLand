using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Entity.ForeignDtos.Request
{
    public class ReqPageBase : ReqBase
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }
    }
}