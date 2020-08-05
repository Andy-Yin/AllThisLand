using System;
using System.ComponentModel.DataAnnotations;
using Lhs.Entity.DbEntity.DbModel;

namespace BattleEngine
{
    public class Hero
    {
        public Hero()
        {
        }

        public void InitFromTero(T_Hero hero)
        {

        }

        /// <summary>
        /// 武将数据库Id
        /// </summary>
        public T_Hero HeroEntity { get; set; }



        /// <summary>
        /// 当前普通攻击
        /// </summary>
        public int CurrentAtk { get; set; }

        /// <summary>
        /// 当前防御
        /// </summary>
        public string CurrentDefend { get; set; }

        /// <summary>
        /// 当前攻城
        /// </summary>
        public string CurrentSiege { get; set; }

        /// <summary>
        /// 当前速度
        /// </summary>
        public string CurrentSpeed { get; set; }

        /// <summary>
        /// 当前魔法攻击
        /// </summary>
        public string CurrentMAtk { get; set; }

        /// <summary>
        /// 战法1
        /// </summary>
        public string FirstMagic { get; set; }

        /// <summary>
        /// 战法2
        /// </summary>
        public int SecondMagic { get; set; }

        /// <summary>
        /// 战法3
        /// </summary>
        public int ThirdMagic { get; set; }
    }
}
