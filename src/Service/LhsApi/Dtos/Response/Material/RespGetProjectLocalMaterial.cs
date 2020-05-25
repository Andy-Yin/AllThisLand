using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Material
{
    /// <summary>
    /// 地采管理返回页面
    /// </summary>
    public class LocalMaterial
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        [DataMember]
        public int SecondCategoryId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [DataMember]
        public string SecondCategoryName { get; set; }

        /// <summary>
        /// 测量
        /// </summary>
        public EnumStatusStage MeasureStatus { get; set; }

        /// <summary>
        /// 下单
        /// </summary>
        public EnumStatusStage OrderStatus { get; set; }

        /// <summary>
        /// 订单确认
        /// </summary>
        public EnumStatusStage ConfirmOrderStatus { get; set; }

        /// <summary>
        /// 确认收货
        /// </summary>
        public EnumStatusStage ConfirmReceiveStatus { get; set; }

        /// <summary>
        /// 安装
        /// </summary>
        public EnumStatusStage InstallStatus { get; set; }

        /// <summary>
        /// 费用结算
        /// </summary>
        public EnumStatusStage SettlementStatus { get; set; }
    }
}
