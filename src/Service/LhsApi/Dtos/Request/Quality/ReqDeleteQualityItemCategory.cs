using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqDeleteQualityItemCategory : ReqAuth
    {
        /// <summary>
        /// 要删除的id
        /// </summary>
        public List<int> Ids { get; set; }
    }
}
