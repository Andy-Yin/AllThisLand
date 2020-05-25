using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 质检审批记录
    /// </summary>
    [Table("T_ProjectQualityApprovalRecord")]
    public class T_ProjectQualityApprovalRecord
    {
        public T_ProjectQualityApprovalRecord()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 审批人Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int QualityRecordId { get; set; }

        /// <summary>
        /// 审批结果：1 同意 0 申诉
        /// </summary>
        public bool Result { get; set; }

        /// <summary>
        /// 备注/验收说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片地址：|分隔
        /// </summary>
        public string Imgs { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 用作系统备注
        /// </summary>
        public string Note { get; set; }
    }
}
