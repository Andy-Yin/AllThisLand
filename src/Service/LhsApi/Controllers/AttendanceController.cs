using EasyCaching.Core;
using Lhs.Common;
using Lhs.Entity.ForeignDtos.Request.Attendance;
using Lhs.Interface;
using LhsAPI;
using LhsAPI.Authorization.Jwt;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 项目相关的controller
    /// </summary>
    [Route("api/attend")]
    //[AuthFilter]
    [ApiController]
    public class AttendanceController : PlatformControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IJwtAppService _jwtApp;
        private readonly IEasyCachingProvider _provider;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public AttendanceController(IEasyCachingProvider provider, IJwtAppService jwtAppService,
            IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
            _jwtApp = jwtAppService;
            _provider = provider;
        }

        /// <summary>
        /// APP提交打卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("checkIn")]
        [AllowAnonymous]
        public async Task<ResponseMessage> CheckIn(ClockInRequest request)
        {
            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = CommonMessage.SubmitFailure,
                Data = ""
            };
            if (request.UserId <= 0)
            {
                result.ErrMsg = "用户ID不正确";
                return result;
            }
            if (request.ProjectId <= 0)
            {
                result.ErrMsg = "项目ID不正确";
                return result;
            }
            var response = await _attendanceRepository.CheckIn(request);
            if (response)
            {
                result.ErrCode = MessageResultCode.Success;
                result.ErrMsg = CommonMessage.SubmitSuccess;
            }
            return result;
        }

        /// <summary>
        /// APP获取某个月份下有打卡记录的日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("month")]
        [AllowAnonymous]
        public async Task<ResponseMessage> MonthRecords([FromQuery]MonthRecordRequest request)
        {
            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = CommonMessage.OperateFailed,
                Data = ""
            };
            if (request.UserId <= 0)
            {
                result.ErrMsg = "用户ID不正确";
                return result;
            }
            if (request.ProjectId <= 0)
            {
                result.ErrMsg = "项目ID不正确";
                return result;
            }
            var response = await _attendanceRepository.MonthRecords(request);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = response;
            return result;
        }
    }
}
