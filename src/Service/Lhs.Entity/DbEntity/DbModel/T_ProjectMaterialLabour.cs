using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectMaterialLabour
    /// </summary>
    [Table("T_ProjectMaterialItem")]
    public class T_ProjectMaterialLabour
    {
        public T_ProjectMaterialLabour()
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
        /// 最终数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double MaterialPrice { get; set; }

        /// <summary>
        /// 行Id
        /// </summary>
        public string DetailsId { get; set; }

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
        /// 工种id
        /// </summary>
        public int WorkTypeId { get; set; }
    }
}
