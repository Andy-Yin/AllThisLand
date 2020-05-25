using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ConstructionPlanTemplateItem
    /// </summary>
    [Table("T_ConstructionPlanTemplateItem")]
    public class T_ConstructionPlanTemplateItem
    {
        public T_ConstructionPlanTemplateItem()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

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
