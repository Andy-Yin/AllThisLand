using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response.Project
{
    public class UserProjectInfo : T_Project
    {
        /// <summary>
        /// 项目经理、工长姓名
        /// </summary>
        public string ConstructionMasterName { get; set; }

        /// <summary>
        /// 项目经理、工长手机号
        /// </summary>
        public string ConstructionMasterPhone { get; set; }

        /// <summary>
        /// 监理姓名
        /// </summary>
        public string SupervisionName { get; set; }

        /// <summary>
        /// 监理手机号
        /// </summary>
        public string SupervisionPhone { get; set; }

        /// <summary>
        /// 设计师姓名
        /// </summary>
        public string DesignerName { get; set; }

        /// <summary>
        /// 设计师手机号
        /// </summary>
        public string DesignerPhone { get; set; }

        public string QuotationId { get; set; } = "";
    }
}
