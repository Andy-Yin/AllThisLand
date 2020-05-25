using System;
using System.Collections.Generic;
using System.Linq;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;
using LhsApi.Dtos.Request;

namespace LhsApi.Dtos.Response.Material
{
    public class RespGetOrderTaskDetail
    {
        public RespGetOrderTaskDetail(
           T_ProjectMaterialItem materialItem,
           T_ProjectOrderTask task,
           ProjectUser user)
        {
            MaterialName = materialItem.MaterialName;
            TaskNo = task.TaskNo;
            IsUrgency = task.IsUrgency;
            PlanUseTime = task.PlanUseTime.ToString("yyyy-MM-dd");
            if (task.Status == EnumOrderTaskStatus.Canceled)
            {
                CancelReason = "是";
            }
            else
            {
                CancelReason = "否";
            }

            ProjectMaterialItemId = task.ProjectMaterialItemId;
            Remark = task.Remark;
            OrderAmount = task.OrderAmount;
            PayAmount = task.PayAmount;

            Quantity = task.Quantity;
            Space = materialItem.Space;
            Unit = materialItem.Unit;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目物料表的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 任务状态：
        /// </summary>
        public EnumOrderTaskStatus Status { get; set; }

        /// <summary>
        /// 预计用货时间
        /// </summary>
        public string PlanUseTime { get; set; }

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
        /// 支付金额
        /// </summary>
        public string PayAmount { get; set; }

        /// <summary>
        /// 取消原因，取消订单时用到
        /// </summary>
        public string CancelReason { get; set; } = "";
        /// <summary>
        /// 待测数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 空间
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        public List<OrderTaskItemForResp> TaskItemList { get; set; } = new List<OrderTaskItemForResp>();
    }

    /// <summary>
    /// 订单条目
    /// </summary>
    public class OrderTaskItemForResp
    {

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
