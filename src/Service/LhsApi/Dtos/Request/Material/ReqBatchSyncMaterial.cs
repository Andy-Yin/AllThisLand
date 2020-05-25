using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 同U9同步物料信息
    /// </summary>
    public class ReqBatchSyncMaterial : ReqAuth
    {
        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; }

        public List<BatchSyncMaterialItem> ItemList { get; set; }
    }

    public class BatchSyncMaterialItem
    {
        /// <summary>
        /// 物料类型：1 主材 2 地采 3 物料人工费
        /// </summary>
        [Range(1,3)]
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 同步类型1-新增，2-减少，3-删除
        /// </summary>
        [Range(1,3)]
        public EnumSyncType SyncType { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        [Required()]
        [StringLength(20, MinimumLength = 3)]
        public string MaterialNo { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        [Required()]
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料空间
        /// </summary>
        [Required()]
        public string Space { get; set; }

        /// <summary>
        /// 单位，可以为空
        /// </summary>
        public string Unit { get; set; } = "";

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; } = "";

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

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
        [Required]
        public string DetailsId { get; set; } = "";

        /// <summary>
        /// 物料分类 材料或是定额
        /// </summary>
        [Required]
        public string RowType { get; set; } = "";

        /// <summary>
        /// 是否需要测量
        /// </summary>
        public bool NeedMeasure { get; set; }

        /// <summary>
        /// 是否需要下单
        /// </summary>
        public bool NeedOrder { get; set; }
    }

    public enum EnumSyncType
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }
}
