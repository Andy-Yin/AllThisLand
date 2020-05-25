using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;
using LhsApi.Dtos.Request;

namespace LhsApi.Dtos.Response.Material
{
    public class RespGetInstallTaskDetail
    {
        public RespGetInstallTaskDetail(T_ProjectMaterialItem materialItem,
            T_ProjectInstallTask task,
            ProjectUser user)
        {
            MaterialName = materialItem.MaterialName;
            TaskNo = task.TaskNo;
            Quantity = materialItem.Quantity;
            Space = materialItem.Space;
            Unit = materialItem.Unit;
            InstallDate = task.InstallDate.ToString("yyyy-MM-dd");
            UserName = user.UserName;
            Phone = user.UserPhone;
            Remark = task.Remark;
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
        /// 安装日期
        /// </summary>
        public string InstallDate { get; set; }

        /// <summary>
        /// 测量人姓名
        /// </summary>
        public string UserName { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
