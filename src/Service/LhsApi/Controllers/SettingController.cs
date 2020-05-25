using Lhs.Common;
using Lhs.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;
using LhsApi.Dtos.Request.Setting;
using LhsApi.Dtos.Response;
using LhsAPI.Dtos.Response.Setting;
using LhsAPI.Dtos.Response.User;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AuthFilter]
    public class SettingController : PlatformControllerBase
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IAuditRepository _auditRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public SettingController(IPositionRepository positionRepository, IAuditRepository auditRepository)
        {
            _positionRepository = positionRepository;
            _auditRepository = auditRepository;
        }

        /// <summary>
        /// 获取岗位-后台
        /// </summary>
        [HttpGet("positionList")]
        public async Task<ResponseMessage> PositionList([FromQuery]ReqAuth req)
        {
            var respData = new List<RespPosionInfo>();
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = respData
            };
            var dataList = await _positionRepository.GetAllPosition();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList.Select(n => new RespPosionInfo(n));
            return result;
        }

        /// <summary>
        /// 新增/编辑岗位-后台
        /// </summary>
        [HttpPost("savePosition")]
        public async Task<ResponseMessage> SavePosition(ReqSavePosition req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            var data = new T_Position()
            {
                Id = req.PositionId,
                Name = req.Name,
                Remark = req.Desc
            };
            //编辑
            if (req.PositionId > 0)
            {
                var dbData = await _positionRepository.SingleAsync(req.PositionId);
                data.CreateTime = dbData.CreateTime;
                var saveResult = await _positionRepository.UpdateAsync(data);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _positionRepository.AddAsync(data);
            if (data.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 删除岗位-后台
        /// </summary>
        [HttpPost("deletePosition")]
        public async Task<ResponseMessage> DeletePosition(ReqDeletePosition req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _positionRepository.DeleteBasicItem(req.Ids);
                if (deleteResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 获取某个岗位的权限-后台
        /// </summary>
        [HttpGet("getAllPermissionForPosition")]
        public async Task<ResponseMessage> GetAllPermissionForPosition([FromQuery]ReqGetAllPermissionForPosition req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = new RespGetAllPermissionForPosition()
            };
            if (req.PositionId <= 0)
            {
                return result;
            }
            var menuList = await _positionRepository.GetAllMenu();
            var positionPermission = await _positionRepository.GetPositionPermission(req.PositionId);
            //构造返回信息
            var permissionList = menuList.Where(parent => parent.ParentId == 0).ToList()
                .Select(parent => new RespGetAllPermissionForPosition()
                {
                    Value = parent.Id,
                    Title = parent.Name,
                    Selected = positionPermission.Contains(parent.Id),
                    Children = menuList.Where(child => child.ParentId == parent.Id).ToList()
                        .Select(child => new MenuInfo()
                        {
                            Value = child.Id,
                            Title = child.Name,
                            Selected = positionPermission.Contains(child.Id),
                            Children = menuList.Where(button => button.ParentId == child.Id).ToList()
                                .Select(button => new ButtonInfo()
                                {
                                    Value = button.Id,
                                    Title = button.Name,
                                    Selected = positionPermission.Contains(button.Id)
                                }).ToList()
                        }).ToList()
                }).ToList();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = permissionList;
            return result;
        }

        /// <summary>
        /// 岗位授权-后台
        /// </summary>
        [HttpPost("authorizePosition")]
        public async Task<ResponseMessage> AuthorizePosition(ReqAuthorizePosition req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.Id <= 0)
            {
                return result;
            }
            var saveResult = await _positionRepository.SavePositionPermission(req.Id, req.Values);
            if (saveResult)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 用户编辑岗位-后台
        /// </summary>
        [HttpPost("editUserPosition")]
        public async Task<ResponseMessage> EditUserPosition(ReqEditUserPosition req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.editUserId <= 0)
            {
                return result;
            }
            var saveResult = await _positionRepository.SaveUserPosition(req.editUserId, req.PositionIds);
            if (saveResult)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 获取操作日志-后台
        /// </summary>
        [HttpGet("getLogList")]
        public async Task<ResponseMessage> GetOperateLogList([FromQuery]ReqOperateLog req)
        {
            var response = new RespBasePage()
            {
                DataList = new List<RespOperateLog>()
            };
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };

            var dbReq = new ReqDbOperateLog()
            {
                Page = req.Page,
                StartTime = req.StartTime,
                EndTime = req.EndTime,
                Source = EnumHelper.GetDescription(typeof(SettingEnum.OperateSource), req.Source),
                UserName = req.UserName
            };
            var dataList = await _auditRepository.GetOperateLogList(dbReq);

            response.TotalCount = (int)dataList.TotalCount;
            response.TotalPage = (int)dataList.TotalPages;
            response.DataList = dataList.Items.Select(n => new RespOperateLog(n)).ToList();

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = response;
            return result;
        }
    }
}
