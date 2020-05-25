using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Material
{
    /// <summary>
    /// 主材明细列表，除了安装
    /// </summary>
    public class RespGetProjectMaterialItem
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 任务编号--在“全部”标签用不到
        /// </summary>
        public string No { get; set; } = "";

        /// <summary>
        /// 空间
        /// </summary>
        public string Space { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; } = "";
    }

    /// <summary>
    /// 主材明细列表,待安装单独处理
    /// </summary>
    public class RespGetProjectMaterialItem_Install
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
    }

    public class MaterialItemList
    {
        public MaterialItemList(int status, string statusName)
        {
            Status = status;
            StatusName = statusName;
            ItemList = new ArrayList();
        }

        /// <summary>
        /// 物料状态：1 待测量 2 待下单 3 待订单确认 4 已下单 5 待发货 6 待入库 7 待出库申请 8 待确认收货 9 待安装 10 待工费结算
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 物料状态：1 待测量 2 待下单 3 待订单确认 4 已下单 5 待发货 6 待入库 7 待出库申请 8 待确认收货 9 待安装 10 待工费结算
        /// </summary>
        public string StatusName { get; set; }

        public ArrayList ItemList { get; set; }
    }
}
