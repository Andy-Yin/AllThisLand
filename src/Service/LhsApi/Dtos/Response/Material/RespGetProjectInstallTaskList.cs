using System.Collections;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Material
{
    public class InstallItem
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 安装日期，可为空
        /// </summary>
        public string InstallTime { get; set; } = "";

        /// <summary>
        /// 安装操作人
        /// </summary>
        public string InstallWorker { get; set; }

        /// <summary>
        /// 操作人手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EnumInstallTaskStatus Status { get; set; } 
    }
    /// <summary>
    /// 安装任务
    /// </summary>
    public class InstallTaskList
    {
        public InstallTaskList(int status, string statusName)
        {
            Status = status;
            StatusName = statusName;
            ItemList = new ArrayList();
        }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 待验收 4 已取消 5 已完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 待验收 4 已取消 5 已完成
        /// </summary>
        public string StatusName { get; set; }

        public ArrayList ItemList { get; set; }
    }
}
