using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_InstallTask
    /// </summary>
    [Table("T_ProjectInstallTask")]
    public class T_ProjectInstallTask
    {
        public T_ProjectInstallTask()
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
        /// 状态：1 待开工 2 已开工 3 已完成
        /// </summary>
        public EnumInstallTaskStatus Status { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        public DateTime InstallDate { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public int WorkerId { get; set; }

        public string WorkerName { get; set; }

        /// <summary>
        /// 安装人手机号
        /// </summary>
        public string WorkerPhone { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
    }
}
