using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_LocalMaterialTemplateItem
    /// </summary>
    [Table("T_LocalMaterialTemplateItem")]
    public class T_LocalMaterialTemplateItem
    {
        public T_LocalMaterialTemplateItem()
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
        public int TemplateId
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public int ItemId
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
