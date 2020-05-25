using Core.Util;
using Lhs.Common;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 派工列表
    /// </summary>
    public class RespAssignWorkerList
    {
        /// <summary>
        /// 
        /// </summary>
        public RespAssignWorkerList()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        public RespAssignWorkerList(WorkerInfo worker)
        {
            WorkerId = worker.Id;
            WorkerName = worker.Name;
            Phone = worker.Phone;
            Sex = worker.Sex ? "女" : "男";
            CompanyId = worker.CompanyId;
            CompanyName = worker.CompanyName;
            AssignTime = worker.AssignTime.ToString(CommonMessage.DateFormatYMD);
            WorkTypeName = worker.WorkTypeName;
        }

        /// <summary>
        /// 
        /// </summary>
        public int WorkerId { get; set; }
        /// <summary>
        /// 张三
        /// </summary>
        public string WorkerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 男
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 所属分公司
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 派工时间
        /// </summary>
        public string AssignTime { get; set; }

        /// <summary>
        /// 工种名称
        /// </summary>
        public string WorkTypeName { get; set; } = string.Empty;
    }
}
