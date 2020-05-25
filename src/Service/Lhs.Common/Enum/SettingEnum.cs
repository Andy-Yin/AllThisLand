using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class SettingEnum
    {
        /// <summary>
        /// 操作日志来源
        /// </summary>
        public enum OperateSource
        {
            [Description("Back")]
            Back = 1,

            [Description("App")]
            App = 2,

            [Description("U9")]
            U9 = 3,

            [Description("WeChat")]
            WeChat = 4
        }
    }
}
