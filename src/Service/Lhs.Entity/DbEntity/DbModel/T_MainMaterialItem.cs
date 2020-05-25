using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_MainMaterialItem
    /// </summary>
    [Table("T_MainMaterialItem")]
    public class T_MainMaterialItem
    {
        public T_MainMaterialItem()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 所属类别
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }

        /// <summary>
        /// 匹配规则:分隔符为|
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 是否需要测量
        /// </summary>
        //public bool NeedMeasure { get; set; }

        /// <summary>
        /// 是否需要下单
        /// </summary>
        //public bool NeedOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 
        /// </summary>
        public DateTime EditTime
        {
            get;
            set;
        } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        } = DateTime.Now;
    }
}
