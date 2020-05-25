using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Enum
{
    public class CommonEnum
    {
        /// <summary>
        /// 图片存储路径
        /// </summary>
        public enum ImgChildPath
        {
            /// <summary>
            /// 公共图片
            /// </summary>
            [Description("Common")]
            Common = 0,

            /// <summary>
            /// 施工管理验收记录
            /// </summary>
            [Description("ConstructionManage")]
            ConstructionManage = 1,

            /// <summary>
            /// 跟进日志
            /// </summary>
            [Description("FollowupLog")]
            FollowupLog = 2,

            /// <summary>
            /// 质检管理
            /// </summary>
            [Description("Quality")]
            Quality = 3,

            /// <summary>
            /// 签收
            /// </summary>
            [Description("Sign")]
            Sign = 4
        }
    }
}
