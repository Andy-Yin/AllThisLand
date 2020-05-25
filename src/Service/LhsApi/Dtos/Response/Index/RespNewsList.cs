using System;
using System.Collections.Generic;
using System.Linq;
using Core.Util;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.Project;
using Lhs.Entity.ForeignDtos.Response.User;

namespace LhsAPI.Dtos.Response.Index
{
    public class RespNewsList
    {
        /// <summary>
        /// 消息列表
        /// </summary>
        public List<RespUserApproveMessage> DataList { get; set; } = new List<RespUserApproveMessage>();

    }

    public class RespUserApproveMessage
    {
        public RespUserApproveMessage() { }

        public RespUserApproveMessage(UserApproveMessage approveMessage)
        {
            ProjectId = approveMessage.ProjectId;
            QuotationId = approveMessage.QuotationId;
            ProjectName = approveMessage.ProjectName;
            ApprovalType = EnumHelper.GetDescription(typeof(ApproveEnum.FlowType), approveMessage.Type);
            Status = EnumHelper.GetDescription(typeof(ApproveEnum.ProjectFlowStatus), approveMessage.Status);
            Time = approveMessage.CreateTime.ToString(CommonMessage.DateFormatYMDHMS);
        }

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
        public string ApprovalType { get; set; }

        /// <summary>
        /// 消息状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string Time { get; set; }

    }
}
