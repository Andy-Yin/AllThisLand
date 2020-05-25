using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsAPI.Authorization.Jwt
{
    /// <summary>
    /// JwtAuthorizationDto 是一个 token 信息的传输对象，包含我们创建好的 token 相关信息，用来将 token 信息返回给前台进行使用。
    /// </summary>
    public class JwtAuthorizationDto
    {
        /// <summary>
        /// 授权时间
        /// </summary>
        public long Auths { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long Expires { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        public int UserId { get; set; }
    }
}
