using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Lhs.Interface;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LhsAPI.Middleware
{
    /// <summary>
    /// 请求中间件，能够比较准确的计算接口花费的时间
    /// </summary>
    public class ResponseTimeMiddleware
    {
        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;

        private readonly IAuditRepository _monitorService;

        /// <summary>
        /// 环境
        /// </summary>
        private readonly IWebHostEnvironment _env;

        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="auditRepository"></param>
        /// <param name="env"></param>
        /// <param name="accessor"></param>
        public ResponseTimeMiddleware(RequestDelegate next, IAuditRepository auditRepository,
            IWebHostEnvironment env, IHttpContextAccessor accessor)
        {
            _next = next;
            _monitorService = auditRepository;
            _env = env;
            _accessor = accessor;
        }

        /// <summary>
        /// 记录时间
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            // 启用倒带功能，可以读取
            context.Request.EnableBuffering();
            var path = context.Request.Path.Value;
            if (!path.ToLower().Contains("/api"))
            {
                await _next(context);
            }
            else
            {
                var watch = new Stopwatch();
                watch.Start();
                var userId = Convert.ToInt32(context.Request.Headers["OperatorId"]);
                var source = context.Request.Headers["Source"].ToString();
                var ip = context.Connection.RemoteIpAddress.ToString();
                var controllerName = path.Split("/")[2];
                var reqParamsString = string.Empty;
                string respParamsString;
                var host = context.Request.Host.ToString();
                var method = context.Request.Method;
                // 获取请求body内容
                if (context.Request.Method.Equals("POST"))
                {
                    // 启用倒带功能，就可以让 Request.Body 可以再次读取
                    context.Request.EnableBuffering();
                    var stream = context.Request.Body;
                    if (context.Request.ContentLength != null)
                    {
                        var buffer = new byte[context.Request.ContentLength.Value];
                        await stream.ReadAsync(buffer, 0, buffer.Length);
                        reqParamsString = Encoding.UTF8.GetString(buffer);
                    }
                    context.Request.Body.Position = 0;
                }
                else if (context.Request.Method.Equals("GET"))
                {
                    reqParamsString = context.Request.QueryString.Value;
                }
                // 获取Response.Body内容
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await _next(context);
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    respParamsString = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var copyTask = responseBody.CopyToAsync(originalBodyStream);
                }
                context.Response.OnCompleted(() =>
                {
                    watch.Stop();
                    var executeTime = (double)watch.ElapsedMilliseconds;
                    var saveTask = SaveSystemMonitorSituationAsync(userId, host, path, controllerName, method, source, ip, executeTime, reqParamsString, respParamsString, _env.IsDevelopment());
                    return Task.CompletedTask;
                });
            }
        }

        /// <summary>
        /// 异步执行存储监控信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="hostName"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="methodStr"></param>
        /// <param name="executeTime"></param>
        /// <param name="requestParam"></param>
        /// <param name="responseContent"></param>
        /// <param name="dev"></param>
        /// <returns>返回任务</returns>
        private async Task SaveSystemMonitorSituationAsync(int userId, string hostName, string actionName, string controllerName,
            string methodStr, string source, string ip, double executeTime, string requestParam, string responseContent, bool dev)
        {
            await Task.Run(() =>
            {
                _monitorService.SaveSystemMonitor(userId, hostName, actionName, controllerName, methodStr, source, ip,
                    executeTime, requestParam, responseContent, dev);
            });
        }
    }
}
