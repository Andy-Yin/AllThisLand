namespace Lhs.Entity.ForeignDtos.Response.Project
{
    public class ProjectListResp
    {
        public int Id { get; set; }// 项目id
        public string ProjectNo { get; set; } = "";// 项目编号
        public string CustomerNo { get; set; } = "";// 客户编号
        public string ProjectName { get; set; } = ""; // 项目名称
        public string CustomerPhone { get; set; } = "";
        public string CustomerName { get; set; } = "";// 客户名称
        public string CompanyName { get; set; } = "";
        public string PlanStartTime { get; set; } = "";// 计划开工时间
        public string PlanFinishTime { get; set; } = "";// 计划完工时间
        public string RealStartTime { get; set; } = ""; // 实际开工时间
        public string RealFinishTime { get; set; } = ""; // 实际完工时间
        public int Status { get; set; } // 项目当前状态
        public string DecorateType { get; set; } = ""; // 装修类型
        public string DeliverDepartment { get; set; } = "";//交付部门
        public string DeliverManager { get; set; } = "";//交付经理
        public string Supervisor { get; set; } = "";//监理
        public string Designer { get; set; } = "";//设计师
        /// <summary>
        /// 包状态：1新接入  2流转中 3已完成 4回收站
        /// </summary>
        public int PackageStatus { get; set; }

        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; } = "";
    }
}
