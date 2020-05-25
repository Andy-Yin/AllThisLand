using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_FollowupLogImg
    /// </summary>
    [Table("T_FollowupLogImg")]
    public class T_FollowupLogImg
    {
        public T_FollowupLogImg()
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
        /// 日志id
        /// </summary>
        public int LogId
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
