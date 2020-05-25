using Microsoft.AspNetCore.Builder;
using System;

namespace LhsAPI.Middleware
{
    /// <summary>
    /// 拓展方法 将中间件加入到请求处理通道中
    /// </summary>
    public static class CalculateExecutionTimeMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCalculateExecutionTime(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}
