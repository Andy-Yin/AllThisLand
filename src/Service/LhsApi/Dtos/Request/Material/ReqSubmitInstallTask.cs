using System;
using System.Collections.Generic;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 提交安装任务
    /// </summary>
    public class ReqSubmitInstallTask : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 测量人
        /// </summary>
        public int UserId { get; set; }

        public List<InstallTask> InstallTaskList { get; set; }

    }

    public class InstallTask
    {

        /// <summary>
        /// 项目里的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        /// <summary>
        /// 安装日期
        /// </summary>
        public DateTime InstallDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
