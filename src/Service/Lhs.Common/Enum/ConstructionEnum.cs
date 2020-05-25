using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class ConstructionEnum
    {
        /// <summary>
        /// 项目工种的施工状态
        /// </summary>
        public enum ConstructionStatus
        {
            [Description("未开始")]
            NotStart = 0,

            [Description("进行中")]
            OnGoing = 1,

            [Description("已完成")]
            Finished = 2
        }

        /// <summary>
        /// 不同类型模板对应的表
        /// </summary>
        public enum ConstructionTemplateType
        {
            [Description("T_MainMaterialTemplate")]
            MainMaterial = 1,

            [Description("T_LocalMaterialTemplate")]
            LocalMaterial = 2,

            [Description("T_ConstructionManageTemplate")]
            ConstructionManage = 3,

            [Description("T_ConstructionPlanTemplate")]
            ConstructionPlan = 4,

            [Description("T_AuxMaterialTemplate")]
            AuxMaterial = 5,

            [Description("T_QualityTemplate")]
            Quality = 6
        }
    }
}
