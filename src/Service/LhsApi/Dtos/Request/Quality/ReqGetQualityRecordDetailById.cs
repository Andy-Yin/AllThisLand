using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqGetQualityRecordDetailById : ReqAuth
    {
        /// <summary>
        /// 质检的Id
        /// </summary>
        public int QualityRecordId { get; set; }
    }
}
