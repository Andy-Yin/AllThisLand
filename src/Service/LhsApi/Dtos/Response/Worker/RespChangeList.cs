using Core.Util;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace LhsAPI.Dtos.Response.Worker
{
    /// <summary>
    /// 变更列表
    /// </summary>
    public class ChangeInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public ChangeInfo()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="change"></param>
        public ChangeInfo(WorkerChangeInfo change)
        {
            Id = change.Id;
            ApplyNum = change.ChangeNo;
            ProjectName = change.ProjectName;
            Reason = change.Reason;
            ConstructionManager = change.ConstructionMasterName;
            Supervisor = change.SupervisionName;
            Type = change.Type ? "安装工人" : "施工工人";
            WorkType = change.WorkTypeName;
            OldWorkerId = change.OldWorkerId;
            OldWorker = change.OldWorkerName;
            NewWorkerId = change.NewWorkerId;
            NewWorker = change.NewWorkerName;
            Status = EnumHelper.GetDescription(typeof(WorkerEnum.WorkerChangeCheckType), change.Status);
            CreateTime = change.CreateTime?.ToString(CommonMessage.DateFormatYMDHMS);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplyNum { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 工长
        /// </summary>
        public string ConstructionManager { get; set; }
        /// <summary>
        /// 监理
        /// </summary>
        public string Supervisor { get; set; }
        /// <summary>
        /// 类型：施工工人/安装工人
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string WorkType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OldWorkerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int NewWorkerId { get; set; }
        /// <summary>
        /// 旧工人名称
        /// </summary>
        public string OldWorker { get; set; }
        /// <summary>
        /// 新工人名称
        /// </summary>
        public string NewWorker { get; set; }
        /// <summary>
        /// 状态：例如带审查
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string Reason { get; set; }
    }
}
