using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqGetQualityItemCategoryList : ReqAuth
    {
        /// <summary>
        /// 搜索关键字（对于名字进行搜索，可为空）
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// 父级Id，如果一级（0）
        /// </summary>
        public int ParentId { get; set; } = 0;
    }
}
