using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 项目质检管理记录表
    /// </summary>
    [Table("T_ProjectQualityRecord")]
    public class T_ProjectQualityRecord
    {
        public T_ProjectQualityRecord()
        {
        }

        public int Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 提交用户ID--监理的Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 质检单号（APP显示）
        /// </summary>
        public string QualityNo { get; set; }

        /// <summary>
        /// 质检任务ID，一级分类的Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 质检任务名称，一级分类的名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 标准金额
        /// </summary>
        public double StandardAmmount { get; set; }

        /// <summary>
        /// 已处罚金额
        /// </summary>
        public double PunishedAmmount { get; set; } = 0;

        /// <summary>
        /// 要求整改日期
        /// </summary>
        public DateTime RectifyDate { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EnumProjectQualityRecordStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public bool IsDel { get; set; } = false;

        public DateTime EditTime { get; set; } = DateTime.Now;
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 触犯的条目，用英文逗号隔开:1,2,3
        /// </summary>
        public string QualityItems { get; set; } = "";

        /// <summary>
        /// 驳回0，通过1，未操作2
        /// </summary>
        public EnumApprovalResult ApprovalResult { get; set; }
    }

    /// <summary>
    /// 记录的审批结果
    /// </summary>
    public enum EnumApprovalResult
    {
        /// <summary>
        /// 拒绝
        /// </summary>
        Reject = 0,

        /// <summary>
        /// 通过
        /// </summary>
        Approved = 1,

        /// <summary>
        /// 未操作
        /// </summary>
        NoAction=2,
    }

    /// <summary>
    /// 质检管理状态
    /// 监理新增→工长确认（完成-罚款）
    /// 监理新增→工长不确认→工程部长确认（完成-罚款）
    /// 监理新增→工长不确认→工程部长不确认（完成-不罚款）
    /// </summary>
    public enum EnumProjectQualityRecordStatus
    {
        /// <summary>
        /// 工长审批中
        /// </summary>
        [Description("工长审批中")]
        ConstructionManagerApproving = 1,

        /// <summary>
        /// 工程队长审批中
        /// </summary>
        [Description("工程队长审批中")]
        ProjectManagerApproving = 2,

        /// <summary>
        /// 审批完成
        /// </summary>
        [Description("审批完成")]
        Finished = 3
    }
}
