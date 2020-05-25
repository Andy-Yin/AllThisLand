using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 验收列表
    /// </summary>
    public class RespCheckList
    {
        /// <summary>
        /// 
        /// </summary>
        public RespCheckList()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="task"></param>
        public RespCheckList(CheckTask task)
        {
            CheckId = task.Id;
            CheckTitle = task.TaskName;
            Status = EnumHelper.GetDescription((typeof(ConstructionEnum.ConstructionStatus)), task.Status);
            Worker = task.WorkerName;
            Phone = task.WorkerPhone;
        }

        /// <summary>
        /// id
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string CheckTitle { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 工人名称
        /// </summary>
        public string Worker { get; set; }
        /// <summary>
        /// 工人电话
        /// </summary>
        public string Phone { get; set; }
    }
}
