using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class WorkerEnum
    {
        /// <summary>
        /// 工人变更类型
        /// </summary>
        public enum WorkerChangeType
        {
            [Description("施工工人")]
            ConstructionWorker = 1,

            [Description("安装工人")]
            InstallWorker = 2
        }

        /// <summary>
        /// 工人变更审核状态
        /// </summary>
        public enum WorkerChangeCheckType
        {
            [Description("待审查")]
            ToCheck = 0,

            [Description("已通过")]
            Pass = 1,

            [Description("已驳回")]
            Fail = 2
        }
    }
}
