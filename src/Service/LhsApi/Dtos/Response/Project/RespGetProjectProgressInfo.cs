using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Core.Util;
using Google.Protobuf.WellKnownTypes;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Entity.ForeignDtos.Response.Project;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Response.Project
{
    /// <summary>
    /// 用户的项目
    /// </summary>
    public class RespGetProjectProgressInfo
    {

        /// <summary>
        /// 项目基本信息
        /// </summary>
        public ProgressInfoInProgress ProjectInfo { get; set; }

        /// <summary>
        /// 项目审批信息
        /// </summary>
        public List<RespProgressInfo> ProgressInfo { get; set; }
    }

    public class ProgressInfoInProgress
    {
        public ProgressInfoInProgress() { }

        public ProgressInfoInProgress(T_Project project)
        {
            ProjectName = project.ProjectName;
            ProjectNo = project.ProjectNo;
            CustomerName = project.CustomerName;
            CustomerPhone = project.CustomerPhone;
            PlanStartTime = project.PlanStartDate.ToString(CommonMessage.DateFormatYMD);
            PlanFinishTime = project.PlanEndDate?.ToString(CommonMessage.DateFormatYMD);
        }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 计划开工日期
        /// </summary>
        public string PlanStartTime { get; set; }

        /// <summary>
        /// 计划完工日期
        /// </summary>
        public string PlanFinishTime { get; set; }
    }

    public class RespProgressInfo
    {
        public RespProgressInfo()
        {

        }

        /// <summary>
        /// 当前审批
        /// </summary>
        public RespProgressInfo(CurrentProjectFlow current, int userId)
        {
            Title = current.NodeName;
            if (!string.IsNullOrEmpty(current.PositionName))
            {
                OperatorName = string.IsNullOrEmpty(current.UserName) ? $"{current.PositionName}" : $"{current.PositionName}（{current.UserName}）";
            }
            CurrentStatusName = current.PreNodeId == 0 ? "待提交" : "待审核";
            if (current.UserId == userId)
            {
                CurrentStatus = current.PreNodeId == 0 ? 4 : 5;
            }
            Type = current.Type;
            Step = current.FlowPositionId;
        }

        /// <summary>
        /// 历史审批
        /// </summary>
        public RespProgressInfo(ProjectFlowRecord record)
        {
            Title = record.FlowNodeName;
            Time = record.CreateTime.ToString(CommonMessage.DateFormatYMDHMS);
            if (!string.IsNullOrEmpty(record.FlowPositionName))
            {
                OperatorName = string.IsNullOrEmpty(record.UserName) ? $"{record.FlowPositionName}" : $"{record.FlowPositionName}（{record.UserName}）";
            }
            CurrentStatusName = EnumHelper.GetDescription<ApproveEnum.ProjectFlowStatus>(record.Result);
            CurrentStatus = (int)record.Result;
            Type = record.Type;
            Step = record.FlowPositionId;
            ApprovalRemarks = record.Remark;
        }

        /// <summary>
        /// 当前进度名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; } = string.Empty;

        /// <summary>
        /// 操作人名称：例如工长（陈娇娇）
        /// </summary>
        public string OperatorName { get; set; } = string.Empty;

        /// <summary>
        /// 当前状态，例如发起人（待提交/已提交）、审核人（待审核/已通过/已驳回）
        /// </summary>
        public string CurrentStatusName { get; set; }

        /// <summary>
        /// 当前状态，0 待其他人处理 1 已提交 2 通过 3 驳回 4 待我提交 5 待我审核
        /// </summary>
        public int CurrentStatus { get; set; }

        /// <summary>
        ///  进度类型 （1.收包确认 2.预交底 3.采购信息订单 4.交底验收）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型：1 工长审核 2 家装设计师审核 3 家装设计师主管审核 4 监理审核
        /// 5 家居设计师审核 6 家居设计师主管审核 7工程助理审核 8 工程部长审核 9 客户 
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        public string ApprovalRemarks { get; set; } = string.Empty;
    }
}
