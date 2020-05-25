using Core.Util;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace LhsAPI.Dtos.Response.Worker
{
    /// <summary>
    /// 工种信息
    /// </summary>
    public class RespStopWorkReason
    {
        /// <summary>
        /// 
        /// </summary>
        public RespStopWorkReason()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workType"></param>
        public RespStopWorkReason(T_StopWorkReason reason)
        {
            Id = reason.Id;
            Reason = reason.Content;
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Reason { get; set; }
    }
}
