using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqUpdateMaterialItemsFromU9 : ReqAuth
    {
        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; }

        public List<ReqUpdateMaterialItem> ItemList { get; set; }
    }

    public class ReqUpdateMaterialItem
    {
        /// <summary>
        /// 物料类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        /// 物料空间
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 只可以是以下几种状态,
        /// 3-订单确认
        /// 5-已入库
        /// 7-已发货
        /// 10-工费结算完成
        /// </summary>
        [Range(1,10)]
        public EnumMaterialItemStatus Status { get; set; }
    }

    public class RespUpdateMaterialFromU9
    {
        public RespUpdateMaterialFromU9(string materialNo, string space, string msg)
        {
            MaterialNo = materialNo;
            Space = space;
            Msg = msg;
        }

        public string MaterialNo { get; set; }
        public string Space { get; set; }
        public string Msg { get; set; }
    }
}
