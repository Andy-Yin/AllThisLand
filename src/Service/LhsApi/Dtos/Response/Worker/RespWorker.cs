using Core.Util;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace LhsAPI.Dtos.Response.Worker
{
    /// <summary>
    /// 工人信息
    /// </summary>
    public class RespWorker
    {
        /// <summary>
        /// 
        /// </summary>
        public RespWorker()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="worker"></param>
        public RespWorker(WorkerInfo worker)
        {
            WorkerId = worker.Id;
            WorkerName = worker.Name;
            Phone = worker.Phone;
            Sex = worker.Sex ? "女" : "男";
            CompanyId = worker.CompanyId;
            WorkTypeId = worker.WorkType;
            WorkTypeName = worker.WorkTypeName;
            CompanyName = worker.CompanyName;
        }

        /// <summary>
        /// 工人id 
        /// </summary>
        public int WorkerId { get; set; }
        /// <summary>
        /// 工人名称
        /// </summary>
        public string WorkerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 分公司id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 工种id
        /// </summary>
        public int WorkTypeId { get; set; }
        /// <summary>
        /// 工种名称
        /// </summary>
        public string WorkTypeName { get; set; }
    }
}
