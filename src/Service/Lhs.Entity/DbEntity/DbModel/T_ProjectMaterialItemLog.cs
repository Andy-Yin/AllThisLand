using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectMaterialItemLog
    /// </summary>
    [Table("T_ProjectMaterialItemLog")]
    public class T_ProjectMaterialItemLog
    {
        public T_ProjectMaterialItemLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        public EnumMaterialItemStatus Status { get; set; }

        /// <summary>
        /// 待。。。
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 已。。
        /// </summary>
        public string Remark { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// 日志当前状态
    /// </summary>
    public enum RemarkStatus
    {
        [Description("已测量")]
        ToBeMeasure2 = 2,

        [Description("已下单")]
        ToSubmitOrder3 = 3,

        [Description("已订单确认")]
        ToConfirmOrder4 = 4,

        [Description("已总部发货")]
        ToBeDelivery5 = 5,

        [Description("已分公司入库")]
        ToBeInStorage6 = 6,

        [Description("已出库申请")]
        ToBeOutStorageApply7 = 7,

        [Description("已确认收货")]
        ToBeConfirmReceive8 = 8,

        [Description("已安装")]
        ToBeInstall9 = 9,

        [Description("已工费结算")]
        ToBeSettlement10 = 10
    }
}
