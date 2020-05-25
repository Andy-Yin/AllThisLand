using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 测量任务
    /// </summary>
    [Table("T_ProjectMeasureTask")]
    public class T_ProjectMeasureTask
    {
        public T_ProjectMeasureTask()
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
        /// 物料的Id
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
        /// 任务状态：1 待开工 2 已开工 3 已完成
        /// </summary>
        public EnumMeasureTaskStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; } = false;
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 要求测量日期
        /// </summary>
        public DateTime RequireDateTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// 安装任务状态的枚举值
    /// 施工任务只有一种状态，已完成
    /// </summary>
    public enum EnumInstallTaskStatus
    {
        /// <summary>
        /// 待开工
        /// </summary>
        ///NotStart = 1,

        /// <summary>
        /// 已开工
        /// </summary>
        ///Working = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Finished = 3
    }

    /// <summary>
    /// 测量任务状态的枚举值
    /// 测量任务只有一种状态-已完成，提交就是已完成
    /// </summary>
    public enum EnumMeasureTaskStatus
    {
        /// <summary>
        /// 待开工
        /// </summary>
        ///NotStart = 1,

        /// <summary>
        /// 已开工
        /// </summary>
        ///Working = 2,

        /// <summary>
        /// 已完成
        /// </summary>
        Finished = 3
    }

}
