using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 列表信息
    /// </summary>
    public class RespList
    {
        /// <summary>
        /// 
        /// </summary>
        public RespList()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="construction"></param>
        public RespList(T_ProjectConstructionManage construction)
        {
            WorkTypeId = construction.CategoryId;
            WorkTypeName = construction.CategoryName;
            AssignStatus = EnumHelper.GetDescription((typeof(ConstructionEnum.ConstructionStatus)), construction.AssignWorkerStatus);
            CheckStatus = EnumHelper.GetDescription((typeof(ConstructionEnum.ConstructionStatus)), construction.CheckStatus);
            BorrowStatus = EnumHelper.GetDescription((typeof(ConstructionEnum.ConstructionStatus)), construction.CashStatus);
            SettlementStatus = EnumHelper.GetDescription((typeof(ConstructionEnum.ConstructionStatus)), construction.SettlementStatus);
        }

        /// <summary>
        /// 后台施工管理中维护的工种id
        /// </summary>
        public int WorkTypeId { get; set; }
        /// <summary>
        /// 后台施工管理中维护的工种名称
        /// </summary>
        public string WorkTypeName { get; set; }
        /// <summary>
        /// 派工状态
        /// </summary>
        public string AssignStatus { get; set; }
        /// <summary>
        /// 验收状态
        /// </summary>
        public string CheckStatus { get; set; }
        /// <summary>
        /// 借支状态
        /// </summary>
        public string BorrowStatus { get; set; }
        /// <summary>
        /// 结算状态
        /// </summary>
        public string SettlementStatus { get; set; }
    }
}
