using System;
using System.Collections.Generic;
using System.Linq;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;

namespace LhsApi.Dtos.Response.Material
{
    public class RespGetMeasureTaskDetail
    {
        public RespGetMeasureTaskDetail(
            T_ProjectMaterialItem materialItem,
            T_ProjectMeasureTask task,
            ProjectUser user,
            List<T_ProjectMeasureTaskItem> taskItemList)
        {
            TaskNo = task.TaskNo;
            MaterialName = materialItem.MaterialName;
            Quantity = materialItem.Quantity;
            Space = materialItem.Space;
            Unit = materialItem.Unit;

            // todo 是施工计划里的结束时间减去开始时间
            RequireDateTime = 7;
            PlanStartDateTime = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            PlanEndDateTime = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd");
            UserName = user.UserName;
            Phone = user.UserPhone;

            if (taskItemList.Any())
            {
                foreach (T_ProjectMeasureTaskItem item in taskItemList)
                {
                    var respItem = new MeasureTaskItemForResp();
                    respItem.Amount = item.Amount;
                    respItem.Size = item.Size;
                    respItem.Note = item.Remark;

                    TaskItemList.Add(respItem);
                }
            }

        }
        /// <summary>
        /// 编号
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

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

        /// <summary>
        /// 要求测量日期
        /// </summary>
        public int RequireDateTime { get; set; }

        /// <summary>
        /// 计划开始工期
        /// </summary>
        public string PlanStartDateTime { get; set; }

        /// <summary>
        /// 计划结束工期
        /// </summary>
        public string PlanEndDateTime { get; set; }

        /// <summary>
        /// 测量人姓名
        /// </summary>
        public string UserName { get; set; }

        public string Phone { get; set; }

        public List<MeasureTaskItemForResp> TaskItemList { get; set; } = new List<MeasureTaskItemForResp>();
    }

    /// <summary>
    /// 测量条目
    /// </summary>
    public class MeasureTaskItemForResp
    {
        /// <summary>
        /// 测量尺寸：如1000X2000
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
