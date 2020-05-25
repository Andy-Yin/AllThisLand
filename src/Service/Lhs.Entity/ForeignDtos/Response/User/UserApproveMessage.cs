using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Common;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response.User
{
    public class UserApproveMessage
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 审核类型
        /// </summary>
        public ApproveEnum.FlowType Type { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public ApproveEnum.ProjectFlowStatus Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
