using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqOrderConfirmedFromU9 : ReqAuth
    {
        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; }

        /// <summary>
        /// 物料类型，主材、地采
        /// </summary>
        [Range(1, 2)]
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 物料状态，只能取值4,5,6（订单确认，总部发货，分公司入库）
        /// </summary>
        [Range(5, 7)]
        public EnumOrderStatusFromReq Status { get; set; }

        public List<int> OrderTaskIdList { get; set; }
    }

    public enum EnumOrderStatusFromReq
    {
        /// <summary>
        /// 订单确认
        /// </summary>
        OrderConfirmed = 4,

        /// <summary>
        /// 总部发货
        /// </summary>
        Delivery = 5,

        /// <summary>
        /// 分公司入库
        /// </summary>
        InStorage = 6
    }
}
