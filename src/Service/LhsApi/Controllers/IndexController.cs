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
using LhsAPI.Dtos.Response.Index;
using LhsAPI.Dtos.Response.Setting;
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

        /// <summary>
        /// 测试百度
        /// </summary>
        [HttpGet("testbaidu")]
        public async Task<ResponseMessage> TestBaidu([FromQuery] ReqAuth req)
        {
            var response = new RespProjectStatistics();
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

    }
}
