using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_BattleLog
    /// </summary>
    [Table("T_BattleLog")]
    public class T_BattleLog
    {
        public T_BattleLog()
        {
        }

        public int Id { get; set; }
        public int HeroId { get; set; }
        public string HeroName { get; set; }
        public decimal Atk { get; set; }
        public decimal Defend { get; set; }
        public decimal Siege { get; set; }
        public decimal Speed { get; set; }
        public decimal MAtk { get; set; }

        /// <summary>
        /// 是否兵种相克，-1,0,1（克制对方）
        /// </summary>
        public int Grams { get; set; }
        public decimal AtkAttacked   { get; set; }
        public decimal DefendAttacked { get; set; }
        public decimal SiegeAttacked { get; set; }
        public decimal SpeedAttacked { get; set; }
        public decimal MAtkAttacked { get; set; }

        /// <summary>
        /// 部队数量
        /// </summary>
        public int Troops { get; set; }

        /// <summary>
        /// 被攻击的部队数量
        /// </summary>
        public int TroopsAttacked { get; set; }
        public DateTime CreateTime { get; set; }

        public int HeroAttackedId { get; set; }
        public string HeroAttackedName { get; set; }

        public int TroopsLoss { get; set; }
        public int TroopRemain { get; set; }
    }
}


