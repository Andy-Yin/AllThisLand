using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Entity.DbEntity.DbModel
{
    public class ProjectQualityRecordForAdmin
    {
        public int Id { get; set; }

        public string ProjectNo { get; set; }

        public string ProjectName { get; set; }

        /// <summary>
        /// 工长
        /// </summary>
        public string ConstructionManagerName { get; set; }

        /// <summary>
        /// 监理
        /// </summary>
        public string SupervisorName { get; set; }

        /// <summary>
        /// 质检单号（APP显示）
        /// </summary>
        public string QualityNo { get; set; }

        /// <summary>
        /// 要求整改日期
        /// </summary>
        public DateTime RectifyDate { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public EnumProjectQualityRecordStatus Status { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
