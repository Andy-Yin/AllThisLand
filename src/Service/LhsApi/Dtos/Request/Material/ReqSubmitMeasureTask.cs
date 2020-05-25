using System;
using System.Collections.Generic;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 提交测量任务
    /// </summary>
    public class ReqSubmitMeasureTask : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 测量人
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        public List<MeasureTask> MeasureTaskList { get; set; }
    }

    public class MeasureTask
    {
        /// <summary>
        /// 项目里的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        public List<MeasureTaskItem> TaskItemList { get; set; }
    }

    /// <summary>
    /// 测量条目
    /// </summary>
    public class MeasureTaskItem
    {
        /// <summary>
        /// 测量尺寸：如1000X2000
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
