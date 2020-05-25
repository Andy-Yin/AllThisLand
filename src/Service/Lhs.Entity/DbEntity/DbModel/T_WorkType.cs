using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_WorkType
    /// </summary>
    [Table("T_WorkType")]
    public class T_WorkType
    {
        public T_WorkType()
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
        public string Remark
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

        /// <summary>
        /// 对应的编码
        /// </summary>
        public string No { get; set; }
    }
}
