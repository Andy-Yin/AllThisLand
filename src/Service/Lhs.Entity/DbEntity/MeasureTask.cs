using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.DbEntity
{

    public class MeasureTaskItem
    {
        /// <summary>
        /// 项目里的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        /// <summary>
        /// 要求测量日期
        /// </summary>
        public DateTime RequireDateTime { get; set; }

        public List<SubMeasureTaskItem> TaskItemList { get; set; }
    }

    /// <summary>
    /// 测量条目
    /// </summary>
    public class SubMeasureTaskItem
    {
        public int ProjectMaterialItemId { get; set; }

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
