using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;

namespace LhsApi.Dtos.Response
{
    /// <summary>
    /// 分页返回的基类
    /// </summary>
    public class RespBasePage
    {
        /// <summary>
        /// 总数据条数 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数 
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// 列表 
        /// </summary>
        public object DataList { get; set; }
    }
}
