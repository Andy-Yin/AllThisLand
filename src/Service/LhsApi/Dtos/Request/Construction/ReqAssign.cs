using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 派工
    /// </summary>
    public class ReqAssign : ReqAuth
    {
        /// <summary>
        /// 项目管理id
        /// </summary>
        public int ProjectManageId { get; set; }

        /// <summary>
        /// 工人id
        /// </summary>
        public int TargetUserId { get; set; }

        /// <summary>
        /// 工人姓名
        /// </summary>
        public string WorkerName { get; set; }
        /// <summary>
        /// 工人电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 要添加的工人的工种id
        /// </summary>
        public int WorkType { get; set; }

        /// <summary>
        /// 分公司id
        /// </summary>
        public string CompanyId { get; set; }
    }
}
