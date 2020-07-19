using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_Hero
    /// </summary>
    [Table("T_Hero")]
    public class T_Hero
    {
        public T_Hero()
        {
        }

        public string Name { get; set; }

        public string Cost { get; set; }

        /// <summary>
        /// 星级
        /// </summary>
        [Range(3, 5)]
        public int Star { get; set; }

        /// <summary>
        /// 性别，男-1，女-2
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public EnumCountry Country { get; set; }

        /// <summary>
        /// 兵种
        /// </summary>
        public EnumCorps Corps { get; set; }

        /// <summary>
        ///  攻击距离
        /// </summary>
        public int AtkRange { get; set; }

        /// <summary>
        /// 普通攻击
        /// </summary>
        public int Atk { get; set; }

        /// <summary>
        /// 防御
        /// </summary>
        public string Defend { get; set; }

        /// <summary>
        /// 攻城
        /// </summary>
        public string Siege { get; set; }

        /// <summary>
        /// 速度
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// 魔法攻击
        /// </summary>
        public string MAtk { get; set; }

        /// <summary>
        /// 攻击每级增加
        /// </summary>
        public string AtkInc { get; set; }

        /// <summary>
        /// 防御每级增加
        /// </summary>
        public string DefendInc { get; set; }

        /// <summary>
        /// 攻城每级增加
        /// </summary>
        public string SiegeInc { get; set; }

        /// <summary>
        /// 速度每级增加
        /// </summary>
        public string SpeedInc { get; set; }

        /// <summary>
        /// 魔法攻击每级增加
        /// </summary>
        public string MAtkInc { get; set; }

        /// <summary>
        /// 默认战法
        /// </summary>
        public string DefaultMagicId { get; set; }
    }

    /// <summary>
    /// 国家/阵营
    /// </summary>
    public enum EnumCountry
    {
        [Description("群")] CountryQun = 1,
        [Description("汉")] CountryHan = 2,
        [Description("魏")] CountryWei = 3,
        [Description("蜀")] CountryShu = 4,
        [Description("吴")] CountryWu = 5
    }

    /// <summary>
    /// 兵种
    /// </summary>
    public enum EnumCorps
    {
        [Description("弓兵")]
        CorpsArcher = 1,
        [Description("步兵")]
        CorpsInfantry = 2,
        [Description("骑兵")]
        CorpsCavalry = 3
    }
}


