using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqGetQualityDetail : ReqAuth
    {
        /// <summary>
        /// 搜索关键字（对于名字进行搜索）
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// 细项明细的上级（二级Id）
        /// </summary>
        public int ParentId { get; set; }
    }
}
