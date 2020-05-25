using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ConstructionManageCheckStandard
    /// </summary>
    [Table("T_ConstructionManageCheckStandard")]
    public class T_ConstructionManageCheckStandard
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 施工任务id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
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
