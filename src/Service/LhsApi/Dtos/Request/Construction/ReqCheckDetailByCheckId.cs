using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 验收详情
    /// </summary>
    public class ReqCheckDetailByCheckId : ReqBasePage
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 验收id
        /// </summary>
        public int CheckId { get; set; }
    }
}
