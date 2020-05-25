using Microsoft.AspNetCore.Mvc;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 默认controller
    /// </summary>
    [Route("/")]
    public class HomeController : PlatformControllerBase
    {
        /// <summary>
        /// 默认地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
