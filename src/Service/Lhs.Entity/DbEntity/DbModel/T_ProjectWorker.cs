using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectWorker
    /// </summary>
    [Table("T_ProjectWorker")]
    public class T_ProjectWorker
    {
        public T_ProjectWorker()
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
        /// 项目管理id
        /// </summary>
        public int ProjectManageId
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public int WorkerId
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
        public DateTime EditTime
        {
            get;
            set;
        }        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }        
    }
}
