using System;
using System.Collections.Generic;
using System.Linq;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;
using LhsApi.Dtos.Request;

namespace LhsApi.Dtos.Response.Material
{
    public class RespGetDetail
    {
        public RespGetDetail(
            T_ProjectMaterialItem materialItem)
        {
            MaterialNo = materialItem.MaterialNo;
            MaterialName = materialItem.MaterialName;
            Spec = materialItem.Spec;
            Quantity = materialItem.Quantity;
            Space = materialItem.Space;
            Unit = materialItem.Unit;
        }

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
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 待测量数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }
    }
}
