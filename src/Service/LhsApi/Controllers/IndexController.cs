using System;
using Lhs.Common;
using Lhs.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpGet("getProjectStatistics")]
        public async Task<ResponseMessage> GetProjectStatistics([FromQuery]ReqAuth req)
        {
            var response = new RespProjectStatistics();
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            var dataList = await _projectRepository.GetAllProject();

            //本月统计
            response.AddCount = dataList.Count(n => n.CreateTime.Year == DateTime.Now.Year && n.CreateTime.Month == DateTime.Now.Month).ToString();
            response.PlanStartCount = dataList.Count(n => n.PlanStartDate.Year == DateTime.Now.Year && n.PlanStartDate.Month == DateTime.Now.Month).ToString();
            response.PlanEndCount = dataList.Count(n => n.PlanEndDate?.Year == DateTime.Now.Year && n.PlanEndDate?.Month == DateTime.Now.Month).ToString();
            //todo：这两个数量暂时不知道怎么获取
            response.AddAmount = "0";
            response.SettlementCount = "0";
            //项目统计
            response.TotalCount = dataList.Count.ToString();
            response.ToBeStartCount = dataList.Count(n => n.Status == (int)ProjectEnum.ProjectStatus.WaitStart).ToString();
            response.PreparationCount = dataList.Count(n => n.Status == (int)ProjectEnum.ProjectStatus.Prepare).ToString();
            response.ConstructingCount = dataList.Count(n => n.Status == (int)ProjectEnum.ProjectStatus.Building).ToString();
            response.CompletedCount = dataList.Count(n => n.Status == (int)ProjectEnum.ProjectStatus.Complete).ToString();
            response.StoppedCount = dataList.Count(n => n.Status == (int)ProjectEnum.ProjectStatus.Stop).ToString();
            //项目进度
            response.ApprovingPackageCount = dataList.Count(n => n.FollowId > (int)ProjectConst.ProjectFlowStartId).ToString();
            response.AddPackageCount = dataList.Count(n => n.FollowId == (int)ProjectConst.ProjectFlowStartId).ToString();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = response;
            return result;
        }


        /// <summary>
        /// 首页获取消息列表-后台
        /// </summary>
        [HttpGet("getNewsList")]
        public async Task<ResponseMessage> GetNewsList([FromQuery]ReqAuth req)
        {
            var response = new RespNewsList();
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            if (req.OperatorId <= 0)
            {
                return result;
            }

            var dataList = await _userRepository.GetUserApproveMessageList(req.OperatorId);

            response.DataList = dataList.Select(n => new RespUserApproveMessage(n)).ToList();
            result.Data = response;

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            return result;
        }
    }
}
