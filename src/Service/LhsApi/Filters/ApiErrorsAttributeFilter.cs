using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Lhs.Common;
using Newtonsoft.Json;

namespace LhsAPI
{
    /// <summary>
    /// Apis异常拦截处理类
    /// 参考：https://www.cnblogs.com/tcjiaan/p/8468901.html
    /// </summary>
    public class ApiErrorsAttributeFilter : Attribute, IExceptionFilter, IAsyncExceptionFilter, IFilterMetadata
    {
        /// <summary>
        /// log4net
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// 环境参数
        /// </summary>
        private readonly IHostingEnvironment _env;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiErrorsAttributeFilter(IHostingEnvironment env)
        {
            _env = env;
            log = LogManager.GetLogger(Startup.repository.Name, typeof(ApiErrorsAttributeFilter));
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                string msg = "服务器不能执行此请求。请稍后重试此请求。如果问题依然存在，请与 Web服务器的管理员联系";
                // 判断是否是开发环境
                if (_env.IsDevelopment())
                {
                    msg = context.Exception.Message;
                }
                context.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new ResponseMessage
                    {
                        ErrCode = MessageResultCode.Error,
                        ErrMsg = msg,
                        Data = new object()
                    }),
                    StatusCode = StatusCodes.Status200OK,
                    ContentType = "application/json;charset=utf-8"
                };
                log.Error(msg);
            }
            //异常已处理了
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// 异步处理异常
        /// </summary>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}
