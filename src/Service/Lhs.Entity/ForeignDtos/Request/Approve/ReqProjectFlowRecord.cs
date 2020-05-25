using System;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Request.Project
{

    /// <summary>
    /// 项目审批记录
    /// </summary>
    public class ProjectFlowRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 类型：1 发包确认 2 预交底审批 3 订单采购审批 4 交底验收审批
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 节点id
        /// </summary>
        public int FlowNodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FlowNodeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FlowPositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FlowPositionName { get; set; }

        /// <summary>
        /// 审批结果：1 通过 2 驳回
        /// </summary>
        public ApproveEnum.ProjectFlowStatus Result { get; set; }

        /// <summary>
        /// 备注或者驳回原因
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }

        /// <summary>
        /// 前一个节点
        /// </summary>
        public int PreNodeId { get; set; }
    }

    /// <summary>
    /// 项目审批记录
    /// </summary>
    public class CurrentProjectFlow
    {
        /// <summary>
        /// 类型：1 发包确认 2 预交底审批 3 订单采购审批 4 交底验收审批
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型：1 发包确认 2 预交底审批 3 订单采购审批 4 交底验收审批
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 岗位类型：1 工长审核 2 家装设计师审核 3 家装设计师主管审核 4 监理审核
        /// 5 家居设计师审核 6 家居设计师主管审核 7工程助理审核 8 工程部长审核 9 客户 
        /// </summary>
        public int FlowPositionId { get; set; }

        /// <summary>
        /// 前一节点
        /// </summary>
        public int PreNodeId { get; set; }

        /// <summary>
        /// 审核人id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 审核人手机号
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PositionName { get; set; }
    }

    /// <summary>
    /// 项目审批记录
    /// </summary>
    public class ReqProjectFlowRecord
    {
        /// <summary>
        /// 类型：1 发包确认 2 预交底审批 3 订单采购审批 4 交底验收审批
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型：1 工长审核 2 家装设计师审核 3 家装设计师主管审核 4 监理审核
        /// 5 家居设计师审核 6 家居设计师主管审核 7工程助理审核 8 工程部长审核 9 客户 
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 类型：true 同意 false驳回
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 审核、驳回原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int ApproveUserId { get; set; }

    }
}
