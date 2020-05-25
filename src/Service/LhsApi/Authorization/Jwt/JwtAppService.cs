using EasyCaching.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Lhs.Common.Const;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LhsAPI.Authorization.Jwt
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtAppService : IJwtAppService
    {
        #region Initialize

        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 获取 HTTP 请求上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtAppService(IEasyCachingProvider provider, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _provider = provider;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        #endregion

        #region API Implements

        /// <summary>
        /// 新增 Token
        /// https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
        /// </summary>
        public JwtAuthorizationDto Create(int userId)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));

                DateTime authTime = DateTime.UtcNow;
                var expireMinutes = Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]);
                DateTime expiresAt = authTime.AddMinutes(expireMinutes);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userId.ToString())
                    }), //创建声明信息
                    Issuer = _configuration["Jwt:Issuer"], //Jwt token 的签发者
                    Audience = _configuration["Jwt:Audience"], //Jwt token 的接收者
                    Expires = expiresAt, //过期时间
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256) //创建 token
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Token 信息
                var jwt = new JwtAuthorizationDto
                {
                    UserId = userId,
                    Token = tokenHandler.WriteToken(token),
                    Auths = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    Expires = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                };

                ////生成新的token 字符串
                //var tokenStr = $"{CacheConst.PlatformToken}{jwt.Token}";
                ////把token作为key存储userid
                //_provider.SetAsync(tokenStr, userId, TimeSpan.FromMinutes(expireMinutes));

                return jwt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// 停用 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task DeactivateAsync(string token)
        {
            var tokenStr = $"{CacheConst.PlatformToken}{token}";
            await _provider.RemoveAsync(tokenStr);
        }

        /// <summary>
        /// 停用当前 Token
        /// </summary>
        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentTokenAsync());

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        public async Task<bool> IsActiveAsync(string token)
        {
            var tokenStr = $"{CacheConst.PlatformToken}{token}";
            return await _provider.ExistsAsync(tokenStr);
        }


        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCurrentActiveTokenAsync()
            => await IsActiveAsync(GetCurrentTokenAsync());

        /// <summary>
        /// 刷新 Token
        /// </summary>
        public async Task<JwtAuthorizationDto> RefreshAsync(string token, int userId)
        {
            //var isActive = await IsCurrentActiveTokenAsync(token);
            //if (!isActive)
            //{
            //    return new JwtAuthorizationDto()
            //    {
            //        Token = "未获取到当前 Token 信息"
            //    };
            //}

            // 停用老的token
            await DeactivateAsync(token);
            // 生成新的token 
            var jwt = Create(userId);

            return jwt;
        }

        #endregion

        #region Method

        /// <summary>
        /// 获取 HTTP 请求的 Token 值
        /// </summary>
        private string GetCurrentTokenAsync()
        {
            //http header
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            //token
            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last(); // bearer tokenvalue
        }

        #endregion
    }
}
