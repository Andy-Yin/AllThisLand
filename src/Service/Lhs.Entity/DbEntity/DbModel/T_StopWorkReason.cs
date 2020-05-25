using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_StopWorkReason
    /// </summary>
    [Table("T_StopWorkReason")]
    public class T_StopWorkReason
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 停复工原因
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
