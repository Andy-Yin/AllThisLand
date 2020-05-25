using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ConstructionPlanItem
    /// </summary>
    [Table("T_ConstructionPlanItem")]
    public class T_ConstructionPlanItem
    {
        public T_ConstructionPlanItem()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Contents
        {
            get;
            set;
        }

        /// <summary>
        /// 周期（内控）：天数
        /// </summary>
        public int InnerDays
        {
            get;
            set;
        }

        /// <summary>
        /// 阶段id
        /// </summary>
        public int StageId
        {
            get;
            set;
        }

        /// <summary>
        /// 周期（合同）：天数
        /// </summary>
        public int ContractDays
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditTime
        {
            get;
            set;
        } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            get;
            set;
        } = DateTime.Now;
    }
}
