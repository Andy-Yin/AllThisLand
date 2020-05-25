using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectConstructionPlan
    /// </summary>
    [Table("T_ProjectConstructionPlan")]
    public class T_ProjectConstructionPlan
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
        public int PlanItemId { get; set; }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工期
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 项目的阶段id
        /// </summary>
        public int ProjectStageId { get; set; }

        /// <summary>
        /// 周期（合同）：天数
        /// </summary>
        public int ContractDays { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PlanStartTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? PlanEndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 状态：0 待开工 1 进行中 2 已完成 3 已取消
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }
    }
}
