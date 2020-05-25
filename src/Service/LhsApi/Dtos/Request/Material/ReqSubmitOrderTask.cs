using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 提交订单任务
    /// </summary>
    public class ReqSubmitOrderTask : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 操作者Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 10 常规单    20 补单  30 建店常规单 40 建店补单  50 备货 60 新品上样 70 追加单 80 建店追加单
        /// </summary>
        [Range(10, 80)]
        public int OrderType { get; set; }

        /// <summary>
        /// 0 免审核，1 需要审核
        /// </summary>
        [Range(0, 1)]
        public int Supplier { get; set; }

        public List<OrderTask> OrderTaskList { get; set; }
    }

    public class OrderTask
    {
        /// <summary>
        /// 项目里的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        /// <summary>
        /// 预计用货日期
        /// </summary>
        [Required]
        public DateTime PlanDateTime { get; set; }

        /// <summary>
        /// 下单数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
