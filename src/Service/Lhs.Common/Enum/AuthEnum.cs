using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Common.Enum
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum LoginStatus
    {
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = -1,

        /// <summary>
        /// 用户名不存在
        /// </summary>
        UserNotExist = -2
    }
}
