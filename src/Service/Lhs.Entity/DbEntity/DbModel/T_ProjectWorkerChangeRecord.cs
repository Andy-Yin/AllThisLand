using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectWorkerChangeRecord
    /// </summary>
    [Table("T_ProjectWorkerChangeRecord")]
    public class T_ProjectWorkerChangeRecord
    {
        public T_ProjectWorkerChangeRecord()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 变更申请编号
        /// </summary>
        public string ChangeNo { get; set; }

        /// <summary>
        /// 变更类型：0 施工工人 1 安装工人
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public int WorkType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OldWorkerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NewWorkerId { get; set; }

        /// <summary>
        /// 备注原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 状态：0 待审查 1 通过 2 驳回
        /// </summary>
        public short Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; } = DateTime.Now;

    }
}
