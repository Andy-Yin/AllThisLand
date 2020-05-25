using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_Worker
    /// </summary>
    [Table("T_Worker")]
    public class T_Worker
    {
        public T_Worker()
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
        /// 所属分公司
        /// </summary>
        public string CompanyId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Sex
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int WorkType
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
        public DateTime EditTime
        {
            get;
            set;
        } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        } = DateTime.Now;
    }
}
