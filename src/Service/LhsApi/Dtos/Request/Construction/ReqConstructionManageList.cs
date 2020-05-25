using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 施工管理的列表信息
    /// </summary>
    public class ReqConstructionManageList: ReqBasePage
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 后台施工管理中维护的工种id
        /// </summary>
        public int WorkTypeId { get; set; }
    }
}
