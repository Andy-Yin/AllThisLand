using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_QualityApprovalImg
    /// </summary>
    [Table("T_QualityApprovalImg")]
    public class T_QualityApprovalImg
    {
        public T_QualityApprovalImg()
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
        public int ApprovalRecordId
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
