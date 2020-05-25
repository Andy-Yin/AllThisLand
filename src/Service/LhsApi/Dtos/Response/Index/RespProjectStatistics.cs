using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.Project;

namespace LhsAPI.Dtos.Response.Index
{
    public class RespProjectStatistics
    {
        /// <summary>
        /// 本月新增项目数量
        /// </summary>
        public string AddCount { get; set; }
        /// <summary>
        /// 本月计划开工项目数量
        /// </summary>
        public string PlanStartCount { get; set; }
        /// <summary>
        /// 本月计划完工项目数量
        /// </summary>
        public string PlanEndCount { get; set; }
        /// <summary>
        /// 本月新增项目总额
        /// </summary>
        public string AddAmount { get; set; }
        /// <summary>
        /// 本月结算项目数量
        /// </summary>
        public string SettlementCount { get; set; }
        /// <summary>
        /// 累计项目数量
        /// </summary>
        public string TotalCount { get; set; }
        /// <summary>
        /// 待开工数量
        /// </summary>
        public string ToBeStartCount { get; set; }
        /// <summary>
        /// 准备期数量
        /// </summary>
        public string PreparationCount { get; set; }
        /// <summary>
        /// 在建数量
        /// </summary>
        public string ConstructingCount { get; set; }
        /// <summary>
        /// 已竣工数量
        /// </summary>
        public string CompletedCount { get; set; }
        /// <summary>
        /// 已停工数量
        /// </summary>
        public string StoppedCount { get; set; }
        /// <summary>
        /// 待审核进度包数量
        /// </summary>
        public string ApprovingPackageCount { get; set; }
        /// <summary>
        /// 新收发包数量
        /// </summary>
        public string AddPackageCount { get; set; }
    }
}
