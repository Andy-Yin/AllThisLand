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
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public IndexController(IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// 首页获取项目统计-后台
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
            var dataList = await _projectRepository.GetAllProject();
            var client = new Baidu.Aip.Ocr.Ocr(BaiduKey.API_KEY, BaiduKey.SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = client.AppId;
            return result;
        }
    }
}
