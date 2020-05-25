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
    public class RespWorkType
    {
        /// <summary>
        /// 
        /// </summary>
        public RespWorkType()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workType"></param>
        public RespWorkType(T_WorkType workType)
        {
            WorkTypeId = workType.Id;
            WorkTypeName = workType.Name;
            Desc = workType.Remark;
        }
        /// <summary>
        /// 工种id
        /// </summary>
        public int WorkTypeId { get; set; }
        /// <summary>
        /// 工种名称
        /// </summary>
        public string WorkTypeName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
