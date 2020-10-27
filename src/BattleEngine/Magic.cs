using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Entity.DbEntity.DbModel;

namespace BattleEngine
{
    public class Magic
    {
        public string Name { get; set; }

        public EnumMagicType MagicType { get; set; }

        /// <summary>
        /// 作用距离
        /// </summary>
        public int Range { get; set; }

        /// <summary>
        /// 兵种
        /// </summary>
        public EnumCorps Corps { get; set; }

        /// <summary>
        /// 作用目标
        /// </summary>
        public EnumTarget Target { get; set; }

        /// <summary>
        /// 作用目标数量
        /// </summary>
        public int TargetNum { get; set; } = 1;

        /// <summary>
        /// 初始发动几率
        /// </summary>
        public double PercentInit { get; set; }

        public string Desc { get; set; }
    }

    /// <summary>
    /// 目标
    /// </summary>
    public enum EnumTarget
    {
        /// <summary>
        /// 我军单体
        /// </summary>
        OurOne = 1,

        /// <summary>
        /// 我军全体
        /// </summary>
        OurMany = 2,

        /// <summary>
        /// 敌军单体
        /// </summary>
        EnemyOne = 3,

        /// <summary>
        /// 敌军全体
        /// </summary>
        EnemyMany = 4,

        /// <summary>
        /// 攻击目标
        /// </summary>
        Target = 5,

        /// <summary>
        /// 自己
        /// </summary>
        Self = 6
    }

    /// <summary>
    /// 战法类型
    /// </summary>
    public enum EnumMagicType
    {
        /// <summary>
        /// 主动
        /// </summary>
        ZhiHui = 1,

        /// <summary>
        /// 主动
        /// </summary>
        ZhuDong = 2,

        /// <summary>
        /// 追击
        /// </summary>
        ZhuiJi = 3,

        /// <summary>
        /// 被动
        /// </summary>
        BeiDong = 4
    }
}
