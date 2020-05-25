using System;
using System.Collections.Generic;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 提交领料
    /// </summary>
    public class ReqSubmitPickMaterial : ReqAuth
    {
        public int ProjectId { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        /// 物料空间
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
