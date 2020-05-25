using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.FollowUpLog
{
    public class ReqGetFollowUpLogDetail : ReqAuth
    {
        /// <summary>
        /// 操作人的Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
