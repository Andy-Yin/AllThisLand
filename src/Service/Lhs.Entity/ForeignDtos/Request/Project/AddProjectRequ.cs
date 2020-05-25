using System;

namespace Lhs.Entity.ForeignDtos.Request.Project
{
    /// <summary>
    /// 添加项目
    /// </summary>
    public class AddProjectReq : ReqAuth
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectNo { get; set; }

        /// <summary>
        /// 合同编码
        /// </summary>
        public string ContractNo { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        public string CustomerNo { get; set; }

        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 装修类型
        /// </summary>
        public string DecorateType { get; set; }

        /// <summary>
        /// 计划开工日期
        /// </summary>
        public string PlanStartDate { get; set; }

        /// <summary>
        /// 计划完工日期
        /// </summary>
        public string PlanEndDate { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        public decimal Area { get; set; }

        /// <summary>
        /// 分公司id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 交付部门id
        /// </summary>
        public string DeliverDepartmentId { get; set; }

        /// <summary>
        /// 交付经理id
        /// </summary>
        public string DeliverManagerId { get; set; }

        /// <summary>
        /// 家装设计师id
        /// </summary>
        public string SolidDesignerId { get; set; }

        /// <summary>
        /// 家装部长id
        /// </summary>
        public string SolidDesignerManagerId { get; set; }

        /// <summary>
        /// 家居设计师id
        /// </summary>
        public string SoftDesignerId { get; set; }

        /// <summary>
        /// 家居部长id
        /// </summary>
        public string SoftDesignerManagerId { get; set; }

        /// <summary>
        /// 工长id
        /// </summary>
        public string ConstructionMasterId { get; set; }

        /// <summary>
        /// 监理id
        /// </summary>
        public string SupervisionId { get; set; }

        /// <summary>
        /// 工程助理id
        /// </summary>
        public string ProjectAssistantId { get; set; }

        /// <summary>
        /// 工程部长id
        /// </summary>
        public string ProjectMinisterId { get; set; }

    }
}
