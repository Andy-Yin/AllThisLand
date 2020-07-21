using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_Hero
    /// </summary>
    [Table("T_Magic")]
    public class T_Magic
    {
        public T_Magic()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 兵种
        /// </summary>
        public EnumCorps Corps { get; set; }

        /// <summary>
        ///  攻击距离
        /// </summary>
        public int AtkRange { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public int Target { get; set; }
    }

}


