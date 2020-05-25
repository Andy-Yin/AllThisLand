using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqGetQualityRecordListForAdmin : ReqBasePage
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 每一步的审批状态
        /// </summary>
        public EnumApprovalResult Status { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string SearchKey { get; set; }

        public string minDate { get; set; }

        public string maxDate { get; set; }

        private int pageNum { get; set; }

    }
}
