using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 工人变更审批
    /// </summary>
    public class ReqChangeApprove : ReqAuth
    {
        /// <summary>
        /// 变更id
        /// </summary>
        public int ChangeId { get; set; }

        /// <summary>
        /// 结果：1 通过 2 驳回
        /// </summary>
        public WorkerEnum.WorkerChangeCheckType Status { get; set; }
    }
}
