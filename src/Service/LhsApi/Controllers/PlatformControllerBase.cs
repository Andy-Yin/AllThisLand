using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Interface;
using LhsApi.Dtos.Request;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 平台Controller基类
    /// </summary>
    public class PlatformControllerBase : ControllerBase
    {
        /// <summary>
        /// 获取当前操作者的UserID
        /// ！！！Controller必须要注入IHttpContextAccessor，参考AuthController
        /// </summary>
        protected int GetCurrentUserId(HttpContext context)
        {
            try
            {
                return Convert.ToInt32(context.User.Identity.Name);
            }
            catch (Exception)
            {
                return -1;
            }
        }
        
        /// <summary>
        /// 公用方法，生成鉴权信息，用于调用U9
        /// </summary>
        protected ReqAuth GetAuthInfo()
        {
            var timeSign = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000).ToString();
            string key = Encrypt.MD5($"{timeSign}LHS-JFAPP");
            return new ReqAuth { TimeSign = timeSign, Key = key };
        }
    }
}

