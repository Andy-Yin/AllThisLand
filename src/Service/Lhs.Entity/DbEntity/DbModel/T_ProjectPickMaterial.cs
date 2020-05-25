using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectPickMaterial
    /// </summary>
    [Table("T_ProjectPickMaterial")]
    public class T_ProjectPickMaterial
    {
        public T_ProjectPickMaterial()
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
        /// 领料单号
        /// </summary>
        public string PickNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectMaterialId { get; set; }

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
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 领料状态：1 申请中 2 已完成
        /// </summary>
        public EnumPickMaterialStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public DateTime EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }

    public enum EnumPickMaterialStatus
    {
        /// <summary>
        /// 申请中
        /// </summary>
        [Description("申请中")]
        Apply = 1,

        /// <summary>
        /// 已完成-确认收货
        /// </summary>
        [Description("已完成")]
        Finished = 2
    }
}
