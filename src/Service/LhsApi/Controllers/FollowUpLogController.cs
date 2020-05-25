using System;
using System.Collections.Generic;
using System.Linq;
using EasyCaching.Core;
using Lhs.Common;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Interface;
using LhsAPI.Authorization.Jwt;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Dtos.Request.Disclosure;
using LhsApi.Dtos.Request.FollowUpLog;
using LhsApi.Dtos.Request.Quality;
using LhsAPI.Dtos.Response.Disclosure;
using OfficeOpenXml;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 跟进日志
    /// </summary>
    [Route("api/FollowUpLog")]
    //[Authorize]
    [ApiController]
    public class FollowUpLogController : PlatformControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowUpLogRepository _followUpLogRepository;
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public FollowUpLogController(IUserRepository userRepository, IFollowUpLogRepository followUpLogRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _followUpLogRepository = followUpLogRepository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        /// 新增一条日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addLog")]
        [AllowAnonymous]
        public async Task<ResponseMessage> AddLog(ReqAddFollowUpLog request)
        {
            var log = new T_FollowupLog();
            log.UserId = request.UserId;
            log.ProjectId = request.ProjectId;
            log.Remark = request.Remark;
            log.TypeId = request.TypeId;
            log.Imgs = request.Images;

            await _followUpLogRepository.AddFollowUpLog(log);
            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Success,
                ErrMsg = CommonMessage.OperateSuccess,
                Data = ""
            };
            return result;
        }

        /// <summary>
        /// 跟进日志详情（列表）
        /// </summary>
        [HttpGet("Detail")]
        public async Task<ResponseMessage> Detail([FromQuery] ReqGetFollowUpLogDetail req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var dataListInDb = await _followUpLogRepository.GetFollowUpLogList(req.UserId, req.ProjectId);
            var dataList = new List<RespGetFollowUpLogList>();
            foreach (var item in dataListInDb)
            {
                var dataItemForResult = new RespGetFollowUpLogList();
                dataItemForResult.Id = item.Id;
                dataItemForResult.Remark = item.Remark;
                dataItemForResult.Images = item.Imgs.Split(CommonConst.Separator).ToList().Select(PicHelper.ConcatPicUrl).ToList();
                dataItemForResult.CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:mm");
                //获取用户和角色
                dataItemForResult.UserName = (await GetUser(_projectRepository, req.ProjectId, req.UserId)).RoleAndName;

                var project = await _projectRepository.SingleAsync(req.ProjectId);
                if (project.ActualStartDate != null)
                {
                    var dateDiff = (DateTime.Now.Date - project.ActualStartDate.Value).Days + 1;
                    dataItemForResult.ProjectStage = $"第{dateDiff}天";
                }
                else
                {
                    dataItemForResult.ProjectStage = string.Format("第0天");
                }

                dataList.Add(dataItemForResult);
            }

            dataList = dataList.OrderBy(l => Convert.ToDateTime(l.CreateTime)).ToList();
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList;
            return result;
        }

        /// <summary>
        /// 所有的跟进日志类别
        /// </summary>
        [HttpGet("TypeList")]
        public async Task<ResponseMessage> TypeList()
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var dataInDb = await _followUpLogRepository.GetFollowUpTypeList();

            var dataInResult = new List<RespGetFollowUpTypeList>();
            foreach (T_FollowupType followupType in dataInDb)
            {
                var item = new RespGetFollowUpTypeList(followupType);
                dataInResult.Add(item);
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataInResult;
            return result;
        }


        /// <summary>
        /// 【后台使用】删除日志分类
        /// </summary>
        [HttpPost("deleteFollowUpLogType")]
        public async Task<ResponseMessage> DeleteFollowUpLogType(ReqDeleteFollowUpLogType req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                bool delResult = true;
                foreach (int id in req.Ids)
                {
                    delResult = delResult && (await _followUpLogRepository.DeleteFollowUpLogType(id));
                }

                if (delResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }

                return result;
            }

            return result;
        }

        /// <summary>
        /// 【后台使用】保存一个日志分类
        /// </summary>
        [HttpPost("saveFollowUpLogType")]
        public async Task<ResponseMessage> SaveFollowUpLogType(ReqSaveFollowUpLogType req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var item = new T_FollowupType()
            {
                Id = req.Id,
                Name = req.Name,
                Remark = req.Remark,
                EditTime = DateTime.Now,
                IsDel = false,
                CreateTime = DateTime.Now
            };
            //编辑
            if (req.Id > 0)
            {
                var itemInDb = (await _followUpLogRepository.GetFollowUpTypeList()).First(t => t.Id == req.Id);
                item.CreateTime = itemInDb.CreateTime;
                item.Id = itemInDb.Id;
                var saveResult = await _followUpLogRepository.SaveFollowUpLogType(item);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }

                return result;
            }
            else
            {
                //新增
                await _followUpLogRepository.SaveFollowUpLogType(item);
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
                return result;
            }
        }

    }
}
