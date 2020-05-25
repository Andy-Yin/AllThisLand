using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_DisclosureTemplate
    /// </summary>
    [Table("T_DisclosureTemplate")]
    public class T_DisclosureTemplate
    {
        public T_DisclosureTemplate()
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
