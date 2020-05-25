using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    public class StopWorkRecordForAdmin
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
        /// 0-停工，1-复工
        /// </summary>
        public EnumStopWorkType Type { get; set; }

        /// <summary>
        /// 计划停工或者复工的日期
        /// </summary>
        public DateTime? PlanDate { get; set; }

        /// <summary>
        /// 停工原因/备注
        /// </summary>
        public string Remark { get; set; } = "";

        /// <summary>
        /// 停工天数：只有停工时才有此字段。
        /// </summary>
        public int StopDays { get; set; }

        /// <summary>
        /// 状态 0-停工申请中（审核中） 1-停工审批通过 3-复工申请中 4-复工审批通过
        /// </summary>
        public short Status { get; set; }

        public string ProjectName { get; set; }

        public string ProjectNo { get; set; }

        /// <summary>
        /// 工长
        /// </summary>
        public string ConstructionManagerName { get; set; }

        /// <summary>
        /// 监理
        /// </summary>
        public string SupervisorName { get; set; }

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 20;
    }
}
