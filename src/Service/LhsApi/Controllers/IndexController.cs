using System;
using Lhs.Common;
using Lhs.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.BaiDuAI;
using Core.Data;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using LhsApi.Dtos.Request;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AuthFilter]
    public class IndexController : PlatformControllerBase
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexController()
        {
        }

    }
}
