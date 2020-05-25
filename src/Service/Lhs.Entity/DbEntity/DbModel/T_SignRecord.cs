using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_SignRecord
    /// </summary>
    [Table("T_SignRecord")]
    public class T_SignRecord
    {
        public T_SignRecord()
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
        public int ProjectId
        {
            get;
            set;
        }        

        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplyNo
        {
            get;
            set;
        }        

        /// <summary>
        /// 运单编号
        /// </summary>
        public string WayBillNo
        {
            get;
            set;
        }        

        /// <summary>
        /// 状态：1 待验收 2 已取消 3 已完成
        /// </summary>
        public short Status
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
