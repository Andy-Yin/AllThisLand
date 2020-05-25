using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectConstructionCheckTask
    /// </summary>
    [Table("T_ProjectConstructionCheckTask")]
    public class T_ProjectConstructionCheckTask
    {
        public T_ProjectConstructionCheckTask()
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
        public string TaskNo
        {
            get;
            set;
        }        

        /// <summary>
        /// 管理任务id
        /// </summary>
        public int ManageTaskId
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string ManageTaskName
        {
            get;
            set;
        }        

        /// <summary>
        /// 计划开工日期
        /// </summary>
        public DateTime? PlanStartDate
        {
            get;
            set;
        }        

        /// <summary>
        /// 计划完工日期
        /// </summary>
        public DateTime? PlanEndDate
        {
            get;
            set;
        }        

        /// <summary>
        /// 状态：0 待开工 1 已开工 2 已完成
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
        } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            get;
            set;
        } = DateTime.Now;
    }
}
