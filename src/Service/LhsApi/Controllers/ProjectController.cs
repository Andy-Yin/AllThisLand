using System;
using System.Collections.Generic;
using System.Linq;
using EasyCaching.Core;
using Lhs.Common;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Interface;
using LhsAPI;
using LhsAPI.Authorization.Jwt;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Util;
using Google.Protobuf.WellKnownTypes;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.ForeignDtos.Response.Project;
using LhsAPI;
using LhsAPI.Dtos.Request.Project;
using LhsApi.Dtos.Response;
using LhsAPI.Dtos.Response.Project;
using log4net.Util;
using Microsoft.Extensions.Hosting;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 项目相关的controller
    /// </summary>
    [Route("api/project")]
    [AuthFilter]
    [ApiController]
    public class ProjectController : PlatformControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectConstructionPlanRepository _projectPlanRepository;
        private readonly IUserPositionRepository _userPositionRepo;
        private readonly IProjectFlowRecordRepository _approveFlowRepository;
        private readonly IProjectUserFlowPositionRepository _projectUserFlowPositionRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public ProjectController(IProjectRepository projectRepository, IUserPositionRepository userPositionRepo, IProjectFlowRecordRepository approveFlowRepository
            , IProjectUserFlowPositionRepository projectUserFlowPositionRepository, IProjectConstructionPlanRepository projectPlanRepository)
        {
            _projectRepository = projectRepository;
            _userPositionRepo = userPositionRepo;
            _approveFlowRepository = approveFlowRepository;
            _projectUserFlowPositionRepository = projectUserFlowPositionRepository;
            _projectPlanRepository = projectPlanRepository;
        }

        /// <summary>
        /// 添加项目-U9
        /// </summary>
        [HttpPost("add")]
        public async Task<ResponseMessage> ProjectAdd(AddProjectReq request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var isExist = await _projectRepository.IsPackageExist(request.QuotationId);
            if (isExist)
            {
                result.ErrMsg = CommonMessage.PackageExist;
                return result;
            }
            var flowPositionOfNotExistId = await _projectRepository.IsFlowPositionExist(request.ConstructionMasterId, request.SolidDesignerId, request.SoftDesignerId, request.SupervisionId);
            if (flowPositionOfNotExistId > 0)
            {
                result.ErrMsg = $"{EnumHelper.GetDescription(typeof(ApproveEnum.FlowStep), flowPositionOfNotExistId)}{CommonMessage.FlowPositionNotExist}";
                return result;
            }
            var addResult = await _projectRepository.AddProject(request);
            if (addResult)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
            }
            return result;
        }

        /// <summary>
        /// 项目列表-后台
        /// </summary>
        [HttpPost("list")]
        public async Task<ResponseMessage> ProjectList(ProjectListRequ request)
        {
            var data = await _projectRepository.GetProjectList(request);
            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Success,
                ErrMsg = CommonMessage.GetSuccess,
                Data = data
            };
            return result;
        }

        /// <summary>
        /// 项目列表-后台
        /// </summary>
        [HttpPost("weChat/list")]
        public async Task<ResponseMessage> CustomerProjectList(ReqCustomerProject request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = await _projectRepository.GetCustomerProjectList(request.UserId, request.Name);
            result.ErrCode = MessageResultCode.Success;
            result.ErrMsg = CommonMessage.GetSuccess;
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 提交项目匹配的模板-后台
        /// </summary>
        [HttpPost("saveMatchingTemplate")]
        public async Task<ResponseMessage> SaveMatchingTemplate(ReqSaveMatchingTemplate req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)
            {
                return result;
            }
            var saveResult = false;
            if (req.Type == (int)ProjectEnum.TemplateType.Disclosure || req.Type == (int)ProjectEnum.TemplateType.PreDisclosure)
            {
                saveResult = await _projectRepository.SaveProjectDisclosure(req.ProjectId, req.Type, req.TemplateId);
            }
            else if (req.Type == (int)ProjectEnum.TemplateType.ConstructionManage)
            {
                saveResult = await _projectRepository.SaveProjectConstructionManage(req.ProjectId, req.TemplateId);
            }
            else if (req.Type == (int)ProjectEnum.TemplateType.ConstructionPlan)
            {
                saveResult = await _projectRepository.SaveProjectConstructionPlan(req.ProjectId, req.TemplateId);
            }
            if (!saveResult)
            {
                return result;
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            return result;
        }

        /// <summary>
        /// 项目列表-APP
        /// </summary>
        [HttpGet("UserProjectList")]
        public async Task<ResponseMessage> UserProjectList([FromQuery]ReqUserProject req)
        {
            var response = new RespBasePage()
            {
                DataList = new List<ProjectInfo>()
            };
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            if (req.UserId <= 0)
            {
                return result;
            }
            // 作为工长的审批列表
            var flowList = await _projectUserFlowPositionRepository.GetProjectUserFlowPositionListByUserIdAndType(req.UserId, UserConst.ConstructionMasterPositionId);
            if (flowList.Any())
            {
                var dataList = await _projectRepository.GetUserProjectList(req.UserId, UserConst.ConstructionMasterPositionId, req.Name, req.PageNum, req.Status);
                response.DataList = dataList.Items.Select(n => new ProjectInfo(n)).ToList();
                response.TotalCount = (int)dataList.TotalCount;
                response.TotalPage = (int)dataList.TotalPages;
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = dataList;
                return result;
            }
            // 作为监理的审批列表
            flowList = await _projectUserFlowPositionRepository.GetProjectUserFlowPositionListByUserIdAndType(req.UserId, UserConst.SuperVisorPositionId);
            if (flowList.Any())
            {
                var dataList = await _projectRepository.GetUserProjectList(req.UserId, UserConst.SuperVisorPositionId, req.Name, req.PageNum, req.Status);
                response.DataList = dataList.Items.Select(n => new ProjectInfo(n)).ToList();
                response.TotalCount = (int)dataList.TotalCount;
                response.TotalPage = (int)dataList.TotalPages;
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = dataList;
                return result;
            }
            result.ErrMsg = CommonMessage.NoPermissionForApp;
            return result;
        }

        /// <summary>
        /// 获取项目进度状态等信息-后台
        /// </summary>
        [HttpGet("progressInfo")]
        public async Task<ResponseMessage> GetProjectProgressInfo([FromQuery]ReqGetProjectProgressInfo req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0 || req.OperatorId <= 0)
            {
                return result;
            }
            var projectInfo = await _projectRepository.SingleAsync(req.ProjectId);
            var respInfo = new RespGetProjectProgressInfo
            {
                ProjectInfo = new ProgressInfoInProgress(projectInfo)
            };
            var recordList = await _approveFlowRepository.GetProjectFlowRecord(req.ProjectId);
            respInfo.ProgressInfo = recordList.Select(n => new RespProgressInfo(n)).ToList();
            var currentFlow = await _approveFlowRepository.GetProjectCurrentFlow(req.ProjectId);
            if (currentFlow != null)
            {
                respInfo.ProgressInfo.Add(new RespProgressInfo(currentFlow, req.OperatorId));
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 获取项目匹配的模板-后台
        /// </summary>
        [HttpGet("getMatchingTemplate")]
        public async Task<ResponseMessage> GetMatchingTemplate([FromQuery]ReqGetMatchingTemplate req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)
            {
                return result;
            }
            var matchList = await _projectRepository.GetMatchingTemplate(req.ProjectId);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = matchList.Select(n => new RespGetMatchingTemplate(n)).ToList();
            return result;
        }

        /// <summary>
        /// 获取项目的施工计划-后台/APP
        /// </summary>
        [HttpGet("planProgressList")]
        public async Task<ResponseMessage> PlanProgressList([FromQuery]ReqGetPlanProgressList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.GetFailure,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0 || req.OperatorId <= 0)
            {
                return result;
            }

            var projectInfo = await _projectRepository.SingleAsync(req.ProjectId);
            var planList = await _projectPlanRepository.GetProjectPlans(req.ProjectId);

            var responseData = new List<RespPlanProgress>();
            //基础计划
            var basicPlans = EnumHelper.GetItems<ProjectEnum.ProjectPlanBasicDetail>();
            foreach (var basicPlan in basicPlans)
            {
                var data = new ProjectPlanInfo();
                DateTime? planStartTime = null;
                DateTime? planEndTime = null;
                DateTime? actualStartTime = null;
                DateTime? actualEndTime = null;
                switch (basicPlan.Key)
                {
                    case (int)ProjectEnum.ProjectPlanBasicDetail.ProtectConstruction:
                        data = planList.First(n => n.Type == ProjectEnum.ProjectPlanType.PackageApprove);
                        planStartTime = data.StartTime;
                        planEndTime = data.EndTime;
                        actualStartTime = data.StartTime;
                        actualEndTime = data.EndTime;
                        break;
                    case (int)ProjectEnum.ProjectPlanBasicDetail.StartMeeting:
                        data = planList.First(n => n.Type == ProjectEnum.ProjectPlanType.PackageApprove);
                        planStartTime = data.EndTime;
                        planEndTime = data.EndTime;
                        actualStartTime = data.EndTime;
                        actualEndTime = data.EndTime;
                        break;
                    case (int)ProjectEnum.ProjectPlanBasicDetail.BasicCheck:
                        var dataList = planList.Where(n => n.Type == ProjectEnum.ProjectPlanType.ConstructionManage).ToList();
                        planStartTime = projectInfo.PlanEndDate;
                        planEndTime = projectInfo.PlanEndDate;
                        actualStartTime = dataList.Min(n => n.StartTime);
                        actualEndTime = dataList.Any(n => n.EndTime == null) ? null : dataList.Max(n => n.EndTime);
                        break;
                    case (int)ProjectEnum.ProjectPlanBasicDetail.ToFinished:
                    case (int)ProjectEnum.ProjectPlanBasicDetail.FinishMeeting:
                        planStartTime = projectInfo.PlanEndDate;
                        planEndTime = projectInfo.PlanEndDate;
                        actualStartTime = null;
                        actualEndTime = null;
                        break;
                }
                responseData.Add(new RespPlanProgress()
                {
                    PlanName = basicPlan.Value,
                    PlanStartTime = $"{planStartTime?.ToString(CommonMessage.DateFormatYMDHM)}~{planEndTime?.ToString(CommonMessage.DateFormatYMDHM)}",
                    ActualStartTime = actualStartTime == null ? string.Empty : $"{actualStartTime?.ToString(CommonMessage.DateFormatYMDHM)}~{actualEndTime?.ToString(CommonMessage.DateFormatYMDHM)}",
                    Status = EnumHelper.GetDescription(typeof(ProjectEnum.ProjectPlanStatus), data.Status)
                });
            }
            //审批流计划信息
            responseData.AddRange(planList.Where(n => n.Type < ProjectEnum.ProjectPlanType.ConstructionManage).ToList()
                .Select(n => new RespPlanProgress(n, projectInfo)).ToList());
            //施工管理计划信息
            planList.Where(n => n.Type == ProjectEnum.ProjectPlanType.ConstructionManage).ToList()
                .ForEach(n =>
                {
                    responseData.Add(new RespPlanProgress(n, projectInfo) { PlanName = $"{n.PlanName}施工" });
                    responseData.Add(new RespPlanProgress(n, projectInfo) { PlanName = $"{n.PlanName}验收" });
                });
            //主材地采中的测量、下单、安装中计划信息
            responseData.AddRange(planList.Where(n => n.Type > ProjectEnum.ProjectPlanType.ConstructionManage).ToList()
                .Select(n => new RespPlanProgress(n, projectInfo)).ToList());

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = responseData;
            return result;
        }
    }
}
