using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.DbEntity
{
    public class ProjectMeasureTask
    {
        public int Id { get; set; }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 已完成
        /// </summary>
        public EnumMeasureTaskStatus Status { get; set; }

        /// <summary>
        /// 材料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 空间
        /// </summary>
        public string Space { get; set; }
    }
}
