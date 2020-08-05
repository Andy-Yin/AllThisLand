using System;
using System.Collections.Generic;
using System.Text;

namespace BattleEngine
{
    public class Troop
    {
        public Hero Hero1 { get; set; }
        public Hero Hero2 { get; set; }
        public Hero Hero3 { get; set; }

        /// <summary>
        /// 疾风营
        /// </summary>
        public bool JiFeng { get; set; }

        /// <summary>
        /// 尚武营
        /// </summary>
        public bool ShangWu { get; set; }

        /// <summary>
        /// 军机营
        /// </summary>
        public bool JunJi { get; set; }

        /// <summary>
        /// 铁壁营
        /// </summary>
        public bool TieBi { get; set; }

        /// <summary>
        /// 阵营加成
        /// </summary>
        public int CountryAdd { get; set; }

        /// <summary>
        /// 兵种加成
        /// </summary>
        public int CorpsAdd { get; set; }

        /// <summary>
        /// 称号加成
        /// </summary>
        public int TitleAdd { get; set; }
    }
}
