using Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace Lhs.Interface
{
    /// <summary>
    /// WebApiClient自定义过滤器：Headers添加签名
    /// </summary>
    class SignFilter : ApiActionFilterAttribute
    {
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            AddSignHeader(context);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 请求头添加签名信息
        /// </summary>
        private void AddSignHeader(ApiActionContext context)
        {
            var methodName = context.RequestMessage.RequestUri.AbsolutePath.Split("/").Last();
            var timeStamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            var fixedString = "abfdb3f36565ecb7d944303845392592";
            var tempList = new List<string>() { fixedString.ToLower(), timeStamp.ToString(), methodName.ToLower() };
            tempList.Sort();
            var valueSign = Encrypt.MD5(string.Join("", tempList)).ToLower().Replace("-", "");
            context.RequestMessage.Headers.Add("TimeStamp", timeStamp.ToString());
            context.RequestMessage.Headers.Add("SignsValue", valueSign);
        }
    }
}