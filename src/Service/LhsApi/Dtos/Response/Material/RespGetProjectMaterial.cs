using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Material
{
    /// <summary>
    /// APP主材管理首页展示
    /// </summary>
    public class RespGetProjectMaterial
    {
        /// <summary>
        /// 一级分类的Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 一级分类名称：如一次配送
        /// </summary>
        public string CategoryName { get; set; }

        public List<Material> MaterialList { get; set; } = new List<Material>();
    }

    /// <summary>
    /// 物料
    /// </summary>
    [DataContract]
    public class Material
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

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
        /// 发货
        /// </summary>
        public EnumStatusStage DeliveryStatus { get; set; }

        /// <summary>
        /// 入库
        /// </summary>
        public EnumStatusStage InStorageStatus { get; set; }

        /// <summary>
        /// 出库
        /// </summary>
        public EnumStatusStage OutStorageApplyStatus { get; set; }

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
