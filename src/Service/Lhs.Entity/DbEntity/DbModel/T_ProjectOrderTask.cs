using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectOrderTask
    /// </summary>
    [Table("T_ProjectOrderTask")]
    public class T_ProjectOrderTask
    {
        public T_ProjectOrderTask()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目物料表的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 任务状态：
        /// </summary>
        public EnumOrderTaskStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MaterialTypeId { get; set; }

        /// <summary>
        /// 材料类型
        /// </summary>
        public string MaterialTypeName { get; set; }

        /// <summary>
        /// 预计用货时间
        /// </summary>
        public DateTime PlanUseTime { get; set; }

        /// <summary>
        /// 是否紧急的
        /// </summary>
        public bool IsUrgency { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 下单金额
        /// </summary>
        public string OrderAmount { get; set; }

        /// <summary>
        /// 下单数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public string PayAmount { get; set; }

        /// <summary>
        /// 取消原因，取消订单时用到
        /// </summary>
        public string CancelReason { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public DateTime EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 因为订单任务会使用三个流程（下单，确认，发货，入库），所以要在这里加一个字段
        /// </summary>
        public EnumMaterialItemStatus OrderStatus { get; set; }
    }

    /// <summary>
    /// 订单任务状态的枚举值
    /// </summary>
    public enum EnumOrderTaskStatus
    {
        /// <summary>
        /// 申请中
        /// </summary>
        [Description("申请中")]
        Submit = 1,

        /// <summary>
        /// 已取消
        /// </summary>
        [Description("已取消")]
        Canceled = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Finished = 3
    }
}
