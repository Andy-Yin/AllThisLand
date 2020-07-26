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
        /// 类型，主动、被动等
        /// </summary>
        public EnumMagicType Type { get; set; }

        /// <summary>
        /// 兵种类型
        /// </summary>
        public EnumCorps Corps { get; set; }

        /// <summary>
        ///  攻击距离
        /// </summary>
        public int AtkRange { get; set; }

        /// <summary>
        /// 目标类型
        /// </summary>
        public int TargetType { get; set; }

        /// <summary>
        /// 目标数量
        /// </summary>
        public int TargetNum { get; set; }

        /// <summary>
        /// 发动几率
        /// </summary>
        public double Probability { get; set; }

        /// <summary>
        /// 发动几率每级增加
        /// </summary>
        public double ProbabilityInc { get; set; }
    }

    /// <summary>
    /// 战法类型
    /// </summary>
    public enum EnumMagicType
    {
        /// <summary>
        /// 指挥
        /// </summary>
        ZhiHui = 1,

        /// <summary>
        /// 主动
        /// </summary>
        ZhuDong = 2,

        /// <summary>
        /// 被动
        /// </summary>
        BeiDong = 3,

        /// <summary>
        /// 追击
        /// </summary>
        ZhuiJi = 4
    }
}


