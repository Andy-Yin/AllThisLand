using System;

namespace Lhs.Entity.ForeignDtos.Request.Project
{
    /// <summary>
    /// 提交审批
    /// </summary>
    public class ReqFlowApprove
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
        /// 提交类型： 1 提交 2 同意 3驳回
        /// </summary>
        public int Approved { get; set; }

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
