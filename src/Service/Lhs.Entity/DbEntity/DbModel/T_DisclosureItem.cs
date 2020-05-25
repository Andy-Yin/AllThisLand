using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_DisclosureItem
    /// </summary>
    [Table("T_DisclosureItem")]
    public class T_DisclosureItem
    {
        public T_DisclosureItem()
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
        /// 类型：0预交底 1交底
        /// </summary>
        public bool Type
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
        public DateTime? EditTime
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
