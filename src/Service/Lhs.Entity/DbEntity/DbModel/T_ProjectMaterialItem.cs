using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectMaterialItem
    /// </summary>
    [Table("T_ProjectMaterialItem")]
    public class T_ProjectMaterialItem
    {
        public T_ProjectMaterialItem()
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
        /// 物料类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 二级分类Id，也就是second categoryId
        /// </summary>
        public int SecondCategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecondCategoryName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料空间
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 物料状态
        /// </summary>
        public EnumMaterialItemStatus Status { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 最终数量，提交测量任务后，不可修改！！！
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        public double ComprePrice { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double MaterialPrice { get; set; }

        /// <summary>
        /// 行Id
        /// </summary>
        public string DetailsId { get; set; }

        /// <summary>
        /// 物料分类 材料或是定额
        /// </summary>
        public string RowType { get; set; }

        public bool NeedMeasure { get; set; }

        public bool NeedOrder { get; set; }

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
        public DateTime CreateTime { get; set; } = DateTime.Now;


        /// <summary>
        /// 测量
        /// </summary>
        public bool MeasureStatus { get; set; } = false;

        /// <summary>
        /// 下单
        /// </summary>
        public bool OrderStatus { get; set; } = false;

        /// <summary>
        /// 订单确认
        /// </summary>
        public bool ConfirmOrderStatus { get; set; } = false;

        /// <summary>
        /// 发货
        /// </summary>
        public bool DeliveryStatus { get; set; } = false;

        /// <summary>
        /// 入库
        /// </summary>
        public bool InStorageStatus { get; set; } = false;

        /// <summary>
        /// 出库
        /// </summary>
        public bool OutStorageApplyStatus { get; set; } = false;

        /// <summary>
        /// 确认收货
        /// </summary>
        public bool ConfirmReceiveStatus { get; set; } = false;

        /// <summary>
        /// 安装
        /// </summary>
        public bool InstallStatus { get; set; } = false;

        /// <summary>
        /// 费用结算
        /// </summary>
        public bool SettlementStatus { get; set; } = false;
    }

    /// <summary>
    /// 1 待测量 2 待下单 3 待订单确认 4 待总部发货  5 待分公司入库  6待出库申请 7 待确认收货 8 待安装 9 待工费结算
    /// </summary>
    public enum EnumMaterialItemStatus
    {
        /// <summary>
        /// 待测量
        /// </summary>
        [Description("待测量")]
        ToBeMeasure1 = 1,

        /// <summary>
        /// 待下单
        /// </summary>
        [Description("待下单")]
        ToSubmitOrder2 = 2,

        /// <summary>
        /// 待订单确认
        /// </summary>
        [Description("待订单确认")]
        ToConfirmOrder3 = 3,

        /// <summary>
        /// 待总部发货（地采没有该状态）
        /// </summary>
        [Description("待总部发货")]
        ToBeDelivery4 = 4,

        /// <summary>
        /// 待分公司入库（地采没有该状态）
        /// </summary>
        [Description("待分公司入库")]
        ToBeInStorage5 = 5,

        /// <summary>
        /// 待出库申请（地采没有该状态）
        /// </summary>
        [Description("待出库申请")]
        ToBeOutStorageApply6 = 6,

        /// <summary>
        /// 待确认收货
        /// </summary>
        [Description("待确认收货")]
        ToBeConfirmReceive7 = 7,

        /// <summary>
        /// 待安装
        /// </summary>
        [Description("待安装")]
        ToBeInstall8 = 8,

        /// <summary>
        /// 待工费结算
        /// </summary>
        [Description("待工费结算")]
        ToBeSettlement9 = 9
    }

    /// <summary>
    /// 物料类别
    /// </summary>
    public enum EnumMaterialType
    {
        /// <summary>
        /// 主材
        /// </summary>
        Main = 1,

        /// <summary>
        /// 地采
        /// </summary>
        Local = 2,

        /// <summary>
        /// 人工费
        /// </summary>
        Labour = 3
    }


    public enum EnumStatusStage
    {
        [Description("无此状态")]
        None = 0,

        [Description("未开始")]
        NotStart = 1,

        [Description("进行中")]
        Doing = 2,

        [Description("已完成")]
        Finished = 3
    }
}
