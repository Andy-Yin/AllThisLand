using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_AuxMaterialItem
    /// </summary>
    [Table("T_AuxMaterialItem")]
    public class T_AuxMaterialItem
    {
        public T_AuxMaterialItem()
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
        /// 所属类别
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }        

        /// <summary>
        /// 匹配规则
        /// </summary>
        public string Code
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
