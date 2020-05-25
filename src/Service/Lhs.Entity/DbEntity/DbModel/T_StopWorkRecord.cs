using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_StopWorkRecord
    /// </summary>
    [Table("T_StopWorkRecord")]
    public class T_StopWorkRecord
    {
        public T_StopWorkRecord()
        {
        }

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
        public string Remark { get; set; }

        /// <summary>
        /// 停工天数：只有停工时才有此字段。
        /// </summary>
        public int StopDays { get; set; }

        /// <summary>
        /// 状态 0-停工申请中（审核中） 1-停工审批通过 3-复工申请中 4-复工审批通过
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;
    }

    public enum EnumStopWorkType
    {
        /// <summary>
        /// 停工
        /// </summary>
        Stop = 0,

        /// <summary>
        /// 复工
        /// </summary>
        Start = 1
    }
}
