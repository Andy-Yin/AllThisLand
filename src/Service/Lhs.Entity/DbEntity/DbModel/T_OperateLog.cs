using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_OperateLog
    /// </summary>
    [Table("T_OperateLog")]
    public class T_OperateLog
    {
        public T_OperateLog()
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
        public int UserId
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string Ip
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string OperateType
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string OperateContent
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
