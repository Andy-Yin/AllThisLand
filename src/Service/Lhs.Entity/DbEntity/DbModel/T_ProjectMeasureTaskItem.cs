using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectMeasureTaskItem
    /// </summary>
    [Table("T_ProjectMeasureTaskItem")]
    public class T_ProjectMeasureTaskItem
    {
        public T_ProjectMeasureTaskItem()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 测量尺寸
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
}
