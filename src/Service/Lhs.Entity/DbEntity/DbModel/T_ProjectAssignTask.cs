using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 派工任务
    /// </summary>
    [Table("T_ProjectAssignTask")]
    public class T_ProjectAssignTask
    {
        public T_ProjectAssignTask()
        {
        }

        public int Id { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string TaskNo { get; set; }

        public string TaskName { get; set; }

        public int ProjectId { get; set; }

        /// <summary>
        /// 工长Id
        /// </summary>
        public int ConstructionManager { get; set; }

        /// <summary>
        /// 工人Id
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// 工人名字
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// 性别：1-男，2-女
        /// </summary>
        public bool Male { get; set; }

        /// <summary>
        /// 工人手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 分公司Id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 分公司名字
        /// </summary>
        public string CompanyName { get; set; }

        public bool IsDel { get; set; } = false;

        public DateTime EditTime { get; set; } = DateTime.Now;

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
