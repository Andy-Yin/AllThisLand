using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response.Worker
{
    public class WorkerChangeInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 变更申请编号
        /// </summary>
        public string ChangeNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 后台施工管理维护的工种id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 变更类型：0 施工工人 1 安装工人
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OldWorkerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NewWorkerId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string OldWorkerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string NewWorkerName { get; set; }

        /// <summary>
        /// 备注原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 状态：1 待审查 其他状态待定
        /// </summary>
        public short? Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public short? WorkType { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 工长名称
        /// </summary>
        public string ConstructionMasterName { get; set; }

        /// <summary>
        /// 监理名称
        /// </summary>
        public string SupervisionName { get; set; }

        /// <summary>
        /// 工种名称
        /// </summary>
        public string WorkTypeName { get; set; }
    }
}
