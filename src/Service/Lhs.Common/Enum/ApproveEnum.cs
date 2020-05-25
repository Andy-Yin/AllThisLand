using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class ApproveEnum
    {
        /// <summary>
        /// 项目审批流状态
        /// </summary>
        public enum ProjectFlowStatus
        {
            [Description("待审核")]
            ToSubmit = 0,

            [Description("已提交")]
            Submit = 1,

            [Description("已通过")]
            Pass = 2,

            [Description("已驳回")]
            Fail = 3
        }

        /// <summary>
        /// 审批流类型
        /// </summary>
        public enum FlowType
        {
            /// <summary>
            /// 发包确认
            /// </summary>
            [Description("发包确认")]
            PackageConfirm = 1,

            /// <summary>
            /// 预交底审批
            /// </summary>
            [Description("预交底审批")]
            PreDisclosure = 2,

            /// <summary>
            /// 订单采购审批
            /// </summary>
            [Description("订单采购审批")]
            OrderProcurement = 3,

            /// <summary>
            /// 交底验收审批
            /// </summary>
            [Description("交底验收审批")]
            Disclosure = 4
        }

        /// <summary>
        /// 审批流步骤/岗位
        /// </summary>
        public enum FlowStep
        {
            [Description("工长")]
            ConstructionMaster = 1,

            [Description("家装设计师")]
            SolidDesigner = 2,

            [Description("家装部长")]
            SolidDesignerManager = 3,

            [Description("监理")]
            Supervision = 4,

            [Description("家居设计师")]
            SoftDesigner = 5,

            [Description("家居部长")]
            SoftDesignerManager = 6,

            [Description("工程助理")]
            ProjectAssistant = 7,

            [Description("工程部长")]
            ProjectMinister = 8,

            [Description("客户")]
            Customer = 9
        }

        /// <summary>
        /// 审批流审核结果
        /// </summary>
        public enum ApproveResult
        {
            /// <summary>
            /// 提交
            /// </summary>
            Submit = 1,

            /// <summary>
            /// 通过
            /// </summary>
            Pass = 2,

            /// <summary>
            /// 拒绝
            /// </summary>
            Refuse = 3
        }

        /// <summary>
        /// 回写U9的审批状态
        /// </summary>
        public enum SubmitU9ApproveType
        {
            /// <summary>
            /// 发包确认
            /// </summary>
            ConstructionMasterPass = 1,

            /// <summary>
            /// 发包驳回
            /// </summary>
            ConstructionMasterFail = 2,

            /// <summary>
            /// 客户确认
            /// </summary>
            CustomerPass = 3,

            /// <summary>
            /// 客户驳回
            /// </summary>
            CustomerFail = 4
        }

        /// <summary>
        /// 用户的审核消息状态
        /// </summary>
        public enum UserApproveMessageStatus
        {
            [Description("待审核")]
            Todo = 0,

            [Description("已通过")]
            Pass = 1,

            [Description("已驳回")]
            Fail = 2
        }
    }
}
