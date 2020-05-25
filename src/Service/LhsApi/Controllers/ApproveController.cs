using System;
using System.Linq;
using Lhs.Common;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Interface;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lhs.Common.Enum;
using LhsAPI;
using LhsAPI.Dtos.Request.Approve;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 项目审批流相关的controller
    /// </summary>
    [Route("api/[controller]")]
    [AuthFilter]
    [ApiController]
    public class ApproveController : PlatformControllerBase
    {
        private readonly IProjectFlowRecordRepository _flowRecordRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectConstructionPlanRepository _projectPlanRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public ApproveController(IProjectFlowRecordRepository flowRecordRepository, IUserRepository userRepository, IProjectRepository projectRepository,
            IProjectConstructionPlanRepository projectPlanRepository)
        {
            _flowRecordRepository = flowRecordRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _projectPlanRepository = projectPlanRepository;
        }

        /// <summary>
        /// 项目审批-APP/后台
        /// </summary>
        [HttpPost("projectProgress")]
        public async Task<ResponseMessage> ProjectProgress(ReqProjectProgress req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var flowApprove = new ReqFlowApprove()
            {
                ProjectId = req.ProjectId,
                ApproveUserId = req.ApproveUserId,
                Type = req.Type,
                Step = req.Step,
                Approved = req.Approved,
                Reason = req.Reason
            };
            var userInfo = await _userRepository.SingleAsync(req.ApproveUserId);
            var projectInfo = await _projectRepository.SingleAsync(req.ProjectId);
            var auth = GetAuthInfo();
            // 是否满足开工要求
            if (flowApprove.Type == (int)ApproveEnum.FlowType.Disclosure && flowApprove.Step == (int)ApproveEnum.FlowStep.ConstructionMaster)
            {
                var canStart = await _flowRecordRepository.CanStartProject(projectInfo.ProjectNo, auth);
                if (!canStart)
                {
                    result.ErrMsg = CommonMessage.CannotStartDueToMoney;
                    return result;
                }
            }
            var submitResult = await _flowRecordRepository.SubmitFlowApprove(flowApprove, userInfo.U9UserId, projectInfo.QuotationId, auth);
            if (submitResult)
            {
                //审批已完成时，生成施工计划
                projectInfo = await _projectRepository.SingleAsync(req.ProjectId);
                if (req.Type == (int)ApproveEnum.FlowType.Disclosure && req.Step == (int)ApproveEnum.FlowStep.Customer && projectInfo.FollowId == 0)
                {
                    var planList = await _projectPlanRepository.GetProjectConstructionPlanList(req.ProjectId);
                    if (planList.Any())
                    {
                        var startTime = (projectInfo.ActualStartDate ?? projectInfo.PlanStartDate).Date;
                        planList.ForEach(plan =>
                        {
                            plan.PlanStartTime = startTime;
                            startTime = startTime.AddDays(plan.Days);
                            plan.PlanEndTime = startTime;
                            plan.EditTime = DateTime.Now;
                        });
                        await _projectPlanRepository.UpdateListAsync(planList);
                    }
                }
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
            }
            else
            {
                result.ErrMsg = CommonMessage.ApproveAuthFail;
            }
            return result;
        }
    }
}
