using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 质检管理具体条目(第三级分类）
    /// </summary>
    [Table("T_QualityItem")]
    public class T_QualityItem
    {
        public T_QualityItem()
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

        public string Name
        {
            get;
            set;
        }        

        /// <summary>
        /// 标准金额
        /// </summary>
        public double Amount
        {
            get;
            set;
        }        

        /// <summary>
        /// 上级分类
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }        

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
