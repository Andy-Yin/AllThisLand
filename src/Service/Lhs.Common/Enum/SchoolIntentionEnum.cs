using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class SchoolIntentionEnum
    {
        /// <summary>
        /// 搜索方式 0：全部 1：近7日 2：近30日
        /// </summary>
        public enum SearchType
        {
            /// <summary>
            /// 全部
            /// </summary>
            All = 0,

            /// <summary>
            /// 近7日
            /// </summary>
            NearlySevenDays = 1,

            /// <summary>
            /// 近30日
            /// </summary>
            NearlyThirtyDays = 2
        }

        /// <summary>
        /// 机构转化的状态名称
        /// </summary>
        public enum SchoolTransition
        {
            /// <summary>
            /// 线索机构
            /// </summary>
            [Description("线索机构")]
            ClueSchool = 1,

            /// <summary>
            /// 部署机构
            /// </summary>
            [Description("部署机构")]
            DeployedSchool = 2,

            /// <summary>
            /// 签约机构
            /// </summary>
            [Description("签约机构")]
            SignedSchool = 3,

            /// <summary>
            /// 续费机构
            /// </summary>
            [Description("续费机构")]
            RenewalSchool = 4
        }

        /// <summary>
        /// 重要性
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 紧急
            /// </summary>
            Urgent = 2,

            /// <summary>
            /// 暂缓
            /// </summary>
            Delay = 3,

        }

        /// <summary>
        /// 客户满意度
        /// </summary>
        public enum CustomerSatisfaction
        {
            /// <summary>
            /// 未选择 --
            /// </summary>
            [Description("--")]
            Default = 0,

            /// <summary>
            /// 非常满意
            /// </summary>
            [Description("非常满意")]
            VerySatisfied = 1,

            /// <summary>
            /// 满意
            /// </summary>
            [Description("满意")]
            Satisfied = 2,

            /// <summary>
            /// 一般
            /// </summary>
            [Description("一般")]
            General = 3,

            /// <summary>
            /// 不满意
            /// </summary>
            [Description("不满意")]
            NotSatisfied = 4
        }
    }
}
