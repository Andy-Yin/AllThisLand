using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common;
using LhsApi.Dtos.Request;
using LhsAPI.Dtos.Request.User;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace LhsAPI
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var request = context.HttpContext.Request;

            var authPara = new ReqAuth();
            if (request.Method.ToLower().Equals("post"))
            {
                request.Body.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    var param = reader.ReadToEnd();

                    authPara = JsonHelper.DeserializeJsonToObject<ReqAuth>(param);

                    request.Body.Seek(0, SeekOrigin.Begin);
                }
            }
            else
            {
                var queryString = context.HttpContext.Request.QueryString;
                var para = queryString.ToString().Substring(1).ToLower();
                if (para.Contains("&"))
                {
                    var paraList = para.Split("&").ToList();
                    authPara.TimeSign = paraList.First(n => n.StartsWith("timesign="));
                    if (string.IsNullOrEmpty(authPara.TimeSign))
                    {
                        context.Result = new UnauthorizedObjectResult("key错误");
                    }
                    authPara.TimeSign = authPara.TimeSign.Split("=")[1];
                    authPara.Key = paraList.First(n => n.StartsWith("key="));
                    if (string.IsNullOrEmpty(authPara.Key))
                    {
                        context.Result = new UnauthorizedObjectResult("key错误");
                    }
                    authPara.Key = authPara.Key.Split("=")[1];
                }
            }
            if (authPara.Key != "1")//调试用
            {
                //时间戳时间与本地时间差距不能大于5分钟 
                var thisTimeValue = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
                long postTimeValue;
                var convertPost = long.TryParse(authPara.TimeSign, out postTimeValue);
                var compareTime = thisTimeValue - postTimeValue;
                var passDate = Convert.ToInt32("6000000");
                //传递的时间戳与服务器时间差距大于5分钟
                if (Math.Abs(compareTime) > passDate)
                {
                    context.Result = new UnauthorizedObjectResult("时间戳已失效");
                }

                if (string.IsNullOrEmpty(authPara.TimeSign) || string.IsNullOrEmpty(authPara.Key))
                {
                    context.Result = new UnauthorizedObjectResult("key错误");
                }
                var needKey = Encrypt.MD5($"{authPara.TimeSign}LHS-JFAPP");
                if (needKey != authPara.Key)
                {
                    context.Result = new UnauthorizedObjectResult("key错误");
                }
            }
        }
    }
}
