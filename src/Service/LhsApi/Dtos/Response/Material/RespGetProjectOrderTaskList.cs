using System.Collections;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Material
{
    public class OrderItem
    {
        public int Id { get; set; }

        public EnumOrderTaskStatus Status { get; set; }

        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialTypeName { get; set; }

        public string StatusName { get; set; }

        public string CreateTime { get; set; }

        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 预计用火时间
        /// </summary>
        public string PlanUseTime { get; set; }
    }

    /// <summary>
    /// 订单任务
    /// </summary>
    public class RespGetProjectOrderTaskList
    {
        public RespGetProjectOrderTaskList(int status, string statusName)
        {
            Status = status;
            StatusName = statusName;
            ItemList = new ArrayList();
        }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 待验收 4 已取消 5 已完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 待验收 4 已取消 5 已完成
        /// </summary>
        public string StatusName { get; set; }

        public ArrayList ItemList { get; set; }
    }
}
