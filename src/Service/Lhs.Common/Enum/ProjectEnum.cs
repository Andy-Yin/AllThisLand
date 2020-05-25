using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class ProjectEnum
    {
        /// <summary>
        /// 匹配的模板类型
        /// </summary>
        public enum TemplateType
        {
            /// <summary>
            /// 预交底
            /// </summary>
            Disclosure = 1,

            /// <summary>
            /// 交底验收
            /// </summary>
            PreDisclosure = 2,

            /// <summary>
            /// 施工管理
            /// </summary>
            ConstructionManage = 3,

            /// <summary>
            /// 施工计划
            /// </summary>
            ConstructionPlan = 4
        }

        /// <summary>
        /// 项目状态
        /// </summary>
        public enum ProjectStatus
        {
            [Description("待开工")]
            WaitStart = 1,

            [Description("准备期")]
            Prepare = 2,

            [Description("在建")]
            Building = 3,

            [Description("已竣工")]
            Complete = 4,

            [Description("已停工")]
            Stop = 5
        }

        /// <summary>
        /// 施工计划状态
        /// </summary>
        public enum ProjectPlanStatus
        {
            [Description("待开工")]
            WaitStart = 0,

            [Description("进行中")]
            Started = 1,

            [Description("已完成")]
            Finished = 2,

            [Description("已取消")]
            Canceled = 3
        }

        /// <summary>
        /// 项目发包状态
        /// </summary>
        public enum ProjectPackageStatus
        {
            [Description("新接入")]
            NewPackage = 1,

            [Description("流转中")]
            OnGoing = 2,

            [Description("已完成")]
            Finished = 3,

            [Description("回收站")]
            Canceled = 4
        }

        /// <summary>
        /// 施工计划明细
        /// </summary>
        public enum ProjectPlanDetailBak
        {
            [Description("保护施工")]
            Detail1 = 1,
            [Description("开工仪式")]
            Detail2 = 2,
            [Description("收尾")]
            Detail3 = 3,
            [Description("内部验收")]
            Detail4 = 4,
            [Description("竣工仪式")]
            Detail5 = 5,
            [Description("预交底")]
            Detail6 = 6,
            [Description("水电施工")]
            Detail7 = 7,
            [Description("水电验收")]
            Detail8 = 8,
            [Description("木工施工")]
            Detail9 = 9,
            [Description("瓦工施工")]
            Detail10 = 10,
            [Description("防水验收")]
            Detail11 = 11,
            [Description("中期验收")]
            Detail12 = 12,
            [Description("油工施工")]
            Detail13 = 13,
            [Description("基础验收")]
            Detail14 = 14,
            [Description("卫生清理")]
            Detail15 = 15,
            [Description("家具、主材管理安装")]
            Detail16 = 16,
            [Description("软装下单")]
            Detail17 = 17,
            [Description("交底验收")]
            Detail18 = 18,
            [Description("卫浴下单")]
            Detail19 = 19,
            [Description("瓷砖下单")]
            Detail20 = 20,
            [Description("地板下单")]
            Detail21 = 21,
            [Description("吊顶下单")]
            Detail22 = 22,
            [Description("产品测量")]
            Detail23 = 23
        }

        /// <summary>
        /// 施工计划基础明细
        /// </summary>
        public enum ProjectPlanBasicDetail
        {
            [Description("保护施工")]
            ProtectConstruction = 1,
            [Description("开工仪式")]
            StartMeeting = 2,
            [Description("基础验收")]
            BasicCheck = 3,
            [Description("收尾")]
            ToFinished = 4,
            [Description("竣工仪式")]
            FinishMeeting = 5
        }

        /// <summary>
        /// 施工计划类型
        /// </summary>
        public enum ProjectPlanType
        {
            [Description("发包确认")]
            PackageApprove = 1,
            [Description("家装设计师预交底")]
            PreDisclosureApprove = 2,
            [Description("家装设计师采购订单")]
            OrderApprove = 3,
            [Description("交底验收")]
            DisclosureApprove = 4,
            [Description("施工管理任务")]
            ConstructionManage = 5,
            [Description("测量任务")]
            MeasureTask = 6,
            [Description("下单任务")]
            OrderTask = 7,
            [Description("安装任务")]
            InstallTask = 8
        }
    }
}
