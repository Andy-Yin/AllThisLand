using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace LhsAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            var token = request.Headers["token"].ToString();
            //0. todo request.Body,拿到用户的userid
            //1. 拿到用户信息，匹配角色是否可以访问
            //2. 拿到用户的菜单权限
            //3. 是否有权限
            var defailtrole = "999";
            if (Roles.Contains(defailtrole) == false)
            {
                throw new Exception("暂无访问权限");
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}