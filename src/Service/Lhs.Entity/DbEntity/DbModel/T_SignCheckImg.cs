using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_SignCheckImg
    /// </summary>
    [Table("T_SignCheckImg")]
    public class T_SignCheckImg
    {
        public T_SignCheckImg()
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
        /// 主键id
        /// </summary>
        public int SignCheckId
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string Url
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
        public DateTime? CreateTime
        {
            get;
            set;
        }        
    }
}
