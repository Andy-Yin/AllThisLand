using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ConstructionManageCategory
    /// </summary>
    [Table("T_ConstructionManageCategory")]
    public class T_ConstructionManageCategory
    {
        public T_ConstructionManageCategory()
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
