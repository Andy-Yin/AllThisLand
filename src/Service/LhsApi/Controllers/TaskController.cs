using System.Collections;
using EasyCaching.Core;
using Lhs.Common;
using Lhs.Interface;
using LhsAPI;
using LhsAPI.Authorization.Jwt;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request.Material;
using LhsApi.Dtos.Request.Task;
using LhsApi.Dtos.Response.Material;
using LhsApi.Dtos.Response.Task;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 任务
    /// </summary>
    [Route("api/task")]
    //[AuthFilter]
    [ApiController]
    //[Authorize]
    public class TaskController : PlatformControllerBase
    {
        private readonly IProjectMaterialRepository _projectMaterialRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectMeasureTaskRepository _projectMeasureTaskRepository;
        private readonly IProjectOrderTaskRepository _projectOrderTaskRepository;
        private readonly IProjectInstallTaskRepository _projectInstallTaskRepository;
        private readonly IProjectAssignTaskRepository _projectAssignTaskRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public TaskController(
            IProjectMaterialRepository materialRepository,
            IProjectMeasureTaskRepository measureTaskRepository,
            IUserRepository userRepository,
            IProjectOrderTaskRepository orderTaskRepository,
            IProjectInstallTaskRepository installTaskRepository,
            IProjectAssignTaskRepository assignTaskRepository
        )
        {
            _projectMaterialRepository = materialRepository;
            _projectMeasureTaskRepository = measureTaskRepository;
            _userRepository = userRepository;
            _projectOrderTaskRepository = orderTaskRepository;
            _projectInstallTaskRepository = installTaskRepository;
            _projectAssignTaskRepository = assignTaskRepository;
        }

        /// <summary>
        /// 我的任务列表
        /// </summary>
        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<ResponseMessage> TaskList([FromQuery] ReqGetTaskList request)
        {
            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = CommonMessage.GetFailure,
                Data = ""
            };

            var measureTaskList = await _projectMeasureTaskRepository.GetMeasureTaskListByProjectAndUserId(request.ProjectId, request.UserId);
            var orderTaskList = await _projectOrderTaskRepository.GetOrderTaskListByProjectAndUserId(request.ProjectId, request.UserId);
            var installTaskList = await _projectInstallTaskRepository.GetInstallTaskListByProjectAndUserId(request.ProjectId, request.UserId);
            var assignTaskList = await _projectAssignTaskRepository.GetAssignTaskListByProjectAndUserId(request.ProjectId, request.UserId);

            var resultData = new ArrayList();

            var list_1 = new MaterialItemList(1, "待确认");
            var list_2 = new MaterialItemList(2, "已确认");
            var list_3 = new MaterialItemList(3, "已提交");
            var list_4 = new MaterialItemList(4, "已取消");
            var list_5 = new MaterialItemList(5, "已完成");

            // 测量任务-只有三个，以及对于任务的状态为：
            // 待开工、已开工、已完成
            // 1-待确认、2-已确认、5-已完成（无已提交和已取消）
            foreach (var item in measureTaskList)
            {
                var task = new RespGetTaskItem(item.Id, EnumTaskType.Measure, item.TaskNo,item.TaskName, item.ProjectMaterialItemId);
               
                // 然后加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    //case EnumMeasureTaskStatus.NotStart:
                    //    task.Status = "待测量";
                    //    list_1.ItemList.Add(task);
                    //    break;
                    //case EnumMeasureTaskStatus.Working:
                    //    task.Status = "待完成";
                    //    list_2.ItemList.Add(task);
                    //    break;
                    case EnumMeasureTaskStatus.Finished:
                        task.Status = "已完成";
                        list_5.ItemList.Add(task);
                        break;
                    default:
                        break;
                }
            }

            // 订单任务-只有三个，以及对于任务的状态为：
            // 申请中、已取消、已完成
            // 3-申请中、4-已取消、5-已完成（无待确认、已确认）
            foreach (var item in orderTaskList)
            {
                var task = new RespGetTaskItem(item.Id, EnumTaskType.Order, item.TaskNo, item.TaskName, item.ProjectMaterialItemId);

                // 然后加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    case EnumOrderTaskStatus.Submit:
                        task.Status = "待确认";
                        list_3.ItemList.Add(task);
                        break;
                    case EnumOrderTaskStatus.Canceled:
                        task.Status = "已取消";
                        list_4.ItemList.Add(task);
                        break;
                    case EnumOrderTaskStatus.Finished:
                        task.Status = "已完成";
                        list_5.ItemList.Add(task);
                        break;
                    default:
                        break;
                }
            }

            // 安装任务-只有三个，以及对于任务的状态为：
            // 待开工、已开工、已完成
            // 1-待确认、2-已确认、5-已完成（无已提交和已取消）
            foreach (var item in installTaskList)
            {
                var task = new RespGetTaskItem(item.Id, EnumTaskType.Install, item.TaskNo, item.TaskName, item.ProjectMaterialItemId);

                // 然后加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    //case EnumInstallTaskStatus.NotStart:
                    //    task.Status = "待安装";
                    //    list_1.ItemList.Add(task);
                    //    break;
                    //case EnumInstallTaskStatus.Working:
                    //    task.Status = "待完成";
                    //    list_2.ItemList.Add(task);
                    //    break;
                    case EnumInstallTaskStatus.Finished:
                        task.Status = "已完成";
                        list_5.ItemList.Add(task);
                        break;
                    default:
                        break;
                }
            }

            // 派工任务-只有1个，以及对于任务的状态为：
            // 已完成
            // 5-已完成（无待确认、已确认、已提交/已取消）
            foreach (var item in assignTaskList)
            {
                var task = new RespGetTaskItem(item.Id, EnumTaskType.Assign, item.TaskNo, item.TaskName, 0);
                task.Status = "已完成";
                list_5.ItemList.Add(task);
            }

            // 统一添加到结果上
            resultData.Add(list_1);
            resultData.Add(list_2);
            resultData.Add(list_3);
            resultData.Add(list_4);
            resultData.Add(list_5);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 派工任务详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("AssignTaskDetails")]
        [AllowAnonymous]
        public async Task<ResponseMessage> AssignTaskDetails([FromQuery] ReqGetAssignTaskDetail request)
        {
            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Error,
                ErrMsg = CommonMessage.GetFailure,
                Data = ""
            };

            var task = await _projectAssignTaskRepository.SingleAsync(request.TaskId);
            var tastResp = new RespGetAssignTaskDetail(task);

            result.ErrCode = MessageResultCode.Success;
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.Data = tastResp;
            return result;
        }
    }
}
