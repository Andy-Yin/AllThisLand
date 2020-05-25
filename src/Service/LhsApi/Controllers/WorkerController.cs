using Lhs.Common;
using Lhs.Entity.DbEntity;
using Lhs.Interface;
using LhsAPI.Dtos.Request.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Worker;
using LhsAPI.Dtos.Request.Construction;
using LhsAPI.Dtos.Request.Disclosure;
using LhsAPI.Dtos.Request.Worker;
using LhsApi.Dtos.Response;
using LhsAPI.Dtos.Response.Construction;
using LhsAPI.Dtos.Response.Disclosure;
using LhsApi.Dtos.Response.Worker;
using LhsAPI.Dtos.Response.Worker;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 工人相关
    /// </summary>
    [Route("api/[controller]")]
    //[AuthFilter]
    [ApiController]
    public class WorkerController : PlatformControllerBase
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkTypeRepository _workTypeRepository;
        private readonly IWorkerChangeRepository _workerChangeRepository;
        private readonly IStopWorkReasonRepository _stopWorkReasonRepository;
        private readonly IStopWorkRecordRepository _stopWorkRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkerController(
            IWorkerRepository workerRepository,
            IWorkerChangeRepository workerChangeRepository,
            IWorkTypeRepository workTypeRepository,
            IStopWorkReasonRepository stopWorkReasonRepository,
            IStopWorkRecordRepository stopWorkRepository)
        {
            _workerRepository = workerRepository;
            _workerChangeRepository = workerChangeRepository;
            _workTypeRepository = workTypeRepository;
            _stopWorkReasonRepository = stopWorkReasonRepository;
            _stopWorkRepository = stopWorkRepository;
        }

        /// <summary>
        /// 获取列表：APP只需要参数ProjectId/PageNum
        /// </summary>
        [HttpGet("changelist")]
        public async Task<ResponseMessage> ChangeList([FromQuery]ReqChangeList req)
        {
            var response = new RespBasePage();
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            // 获取模板列表
            var dataList = await _workerRepository.GetAssignWorkerList(req.ProjectId, req.ApplyType, req.PageNum, req.StartTime, req.EndTIme, req.KeyWords);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            response.TotalCount = (int)dataList.TotalCount;
            response.TotalPage = (int)dataList.TotalPages;
            response.DataList = dataList.Items.Select(n => new ChangeInfo(n)).ToList();
            result.Data = response;

            return result;
        }

        /// <summary>
        /// 提交变更申请
        /// </summary>
        [HttpPost("change")]
        public async Task<ResponseMessage> Change(ReqChange req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (!Enum.IsDefined(typeof(WorkerEnum.WorkerChangeType), req.Type) || req.OldWorkerId <= 0 || req.NewWorkerId <= 0)
            {
                return result;
            }
            var worker = await _workerRepository.SingleAsync(req.OldWorkerId);
            var change = new T_ProjectWorkerChangeRecord()
            {
                ChangeNo = $"{CommonConst.NoForWorkerChange}{CommonHelper.GetTimeStamp()}",
                ProjectId = req.ProjectId,
                Type = req.Type,
                WorkType = worker.WorkType,
                OldWorkerId = req.OldWorkerId,
                NewWorkerId = req.NewWorkerId,
                Reason = req.Note
            };
            var addResult = await _workerChangeRepository.AddAsync(change);
            if (addResult <= 0)
            {
                return result;
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";

            return result;
        }

        /// <summary>
        /// 审核工人变更
        /// </summary>
        [HttpPost("change/approve")]
        public async Task<ResponseMessage> ChangeApprove(ReqChangeApprove req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ChangeId <= 0 || (req.Status != WorkerEnum.WorkerChangeCheckType.Pass && req.Status != WorkerEnum.WorkerChangeCheckType.Fail))
            {
                return result;
            }
            //更新审核状态
            var changeInfo = await _workerChangeRepository.SingleAsync(req.ChangeId);
            changeInfo.Status = (short)req.Status;
            changeInfo.EditTime = DateTime.Now;
            var approveResult = await _workerChangeRepository.UpdateAsync(changeInfo);
            //通过时，变更工人
            if (approveResult)
            {
                if (req.Status == WorkerEnum.WorkerChangeCheckType.Pass)
                {
                    var changeResult = await _workerChangeRepository.ChangeWorker(changeInfo.ProjectId, changeInfo.Type, changeInfo.OldWorkerId, changeInfo.NewWorkerId);
                    if (changeResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                }
                else
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 删除工人变更
        /// </summary>
        [HttpPost("deleteChange")]
        public async Task<ResponseMessage> DeleteChange(ReqDeleteChange req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _workerChangeRepository.DeleteChange(req.Ids);
                if (deleteResult)
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
        /// 获取工人列表
        /// </summary>
        [HttpGet("list")]
        public async Task<ResponseMessage> List([FromQuery]ReqWorkerList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取模板列表
            var dataList = new List<WorkerInfo>();
            if (req.ProjectId > 0 && Enum.IsDefined(typeof(WorkerEnum.WorkerChangeType), req.Type))
            {
                dataList = await _workerRepository.GetWorkerList(req.ProjectId, req.Type);
            }
            else
            {
                dataList = await _workerRepository.GetWorkerList(req.CompanyId, req.WorkTypeId, req.KeyWords);
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList.Select(n => new RespWorker(n)).ToList();

            return result;
        }

        /// <summary>
        /// 新增/编辑工人
        /// </summary>
        [HttpPost("addOrEdit")]
        public async Task<ResponseMessage> AddOrEdit(ReqSaveWorker req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var worker = new T_Worker()
            {
                Id = req.WorkerId,
                Name = req.WorkerName,
                WorkType = req.WorkTypeId,
                CompanyId = req.CompanyId,
                Phone = req.Phone,
                Sex = req.Sex
            };
            //编辑
            if (req.WorkerId > 0)
            {
                var dbData = await _workerRepository.SingleAsync(req.WorkerId);
                worker.CreateTime = dbData.CreateTime;
                var saveResult = await _workerRepository.UpdateAsync(worker);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _workerRepository.AddAsync(worker);
            if (worker.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 删除工人
        /// </summary>
        [HttpPost("delete")]
        public async Task<ResponseMessage> Delete(ReqDeleteWorker req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.WorkerIds.Any())
            {
                var deleteResult = await _workerRepository.DeleteWorker(req.WorkerIds);
                if (deleteResult)
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
        /// 新增/编辑工种
        /// </summary>
        [HttpPost("saveWorkType")]
        public async Task<ResponseMessage> SaveWorkType(ReqSaveWorkType req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var workType = new T_WorkType()
            {
                Id = req.WorkTypeId,
                Name = req.WorkTypeName,
                Remark = req.Desc
            };
            //编辑
            if (req.WorkTypeId > 0)
            {
                var dbData = await _workerRepository.SingleAsync(req.WorkTypeId);
                workType.CreateTime = dbData.CreateTime;
                var saveResult = await _workTypeRepository.UpdateAsync(workType);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _workTypeRepository.AddAsync(workType);
            if (workType.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 获取工种列表
        /// </summary>
        [HttpGet("workTypeList")]
        public async Task<ResponseMessage> WorkTypeList()
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var dataList = await _workTypeRepository.GetWorkTypeList();
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList.Select(n => new RespWorkType(n)).ToList();

            return result;
        }

        /// <summary>
        /// 删除工种
        /// </summary>
        [HttpPost("deleteWorkType")]
        public async Task<ResponseMessage> DeleteWorkType(ReqDeleteWorkType req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.WorkTypeId.Any())
            {
                var deleteResult = await _workTypeRepository.DeleteWorkType(req.WorkTypeId);
                if (deleteResult)
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
        /// 获取停复工原因列表
        /// </summary>
        [HttpGet("stopOrStartReasonList")]
        public async Task<ResponseMessage> StopOrStartReasonList()
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var dataList = await _stopWorkReasonRepository.GetStopWorkReasons();
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList.Select(n => new RespStopWorkReason(n)).ToList();

            return result;
        }

        /// <summary>
        /// 后台获取停复工列表
        /// </summary>
        [HttpGet("stopOrStartListForAdmin")]
        public async Task<ResponseMessage> StopOrStartListForAdmin([FromQuery]ReqStopOrStartListForAdmin req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            if (req.StartFromDay == null)
            {
                req.StartFromDay = DateTime.MinValue;
            }
            if (req.StartEndDay == null)
            {
                req.StartEndDay = DateTime.MinValue;
            }
            if (req.StopFromDay == null)
            {
                req.StopFromDay = DateTime.MinValue;
            }
            if (req.StopToDay == null)
            {
                req.StopToDay = DateTime.MinValue;
            }
            var dataResult = await _stopWorkRepository.GetStopWorkRecordList(
                req.Type,
                req.StartFromDay.Value, req.StartEndDay.Value,
                req.StopFromDay.Value, req.StopToDay.Value,
                req.Search, req.pageIndex, req.pageSize);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataResult;

            return result;
        }

        /// <summary>
        /// 获取停复工列表
        /// </summary>
        [HttpGet("stopOrStartListForApp")]
        public async Task<ResponseMessage> StopOrStartListForApp([FromQuery]ReqStopOrStartListForApp req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var sql = $@"select * from T_StopWorkRecord where projectid={req.ProjectId} and isDel = 0";
            var dataList = await _stopWorkRepository.FindAllAsync<T_StopWorkRecord>(sql);
            var dataResult = new List<RespStopOrStartListForApp>();

            if (dataList.Any())
            {
                foreach (T_StopWorkRecord record in dataList)
                {
                    var item = new RespStopOrStartListForApp();
                    if (record.Type == EnumStopWorkType.Start)
                    {
                        item.TypeName = "复工申请";
                    }
                    else
                    {
                        item.TypeName = "停工申请";
                    }

                    item.RecordId = record.Id;
                    //0-停工申请中（审核中） 1-停工审批通过 3-复工申请中 4-复工审批通过
                    item.Status = record.Status;
                    // 计划停工或者复工日期
                    if (record.PlanDate != null) item.PlanDate = record.PlanDate.Value.ToString("yyyy-MM-dd");
                    item.StopDays = record.StopDays;
                    item.StopReason = record.Remark;

                    dataResult.Add(item);
                }
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataResult;

            return result;
        }

        /// <summary>
        /// 新增/编辑停复工原因
        /// </summary>
        [HttpPost("saveStopOrStartReason")]
        public async Task<ResponseMessage> SaveStopOrStartReason(ReqStopWorkReason req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var reason = new T_StopWorkReason()
            {
                Id = req.Id,
                Content = req.Reason
            };
            //编辑
            if (req.Id > 0)
            {
                var dbData = await _stopWorkReasonRepository.SingleAsync(req.Id);
                reason.CreateTime = dbData.CreateTime;
                var saveResult = await _stopWorkReasonRepository.UpdateAsync(reason);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _stopWorkReasonRepository.AddAsync(reason);
            if (reason.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 删除停复工原因
        /// </summary>
        [HttpPost("deleteStopOrStartReason")]
        public async Task<ResponseMessage> DeleteStopOrStartReason(ReqDeleteStopReason req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _stopWorkReasonRepository.DeleteWorkType(req.Ids);
                if (deleteResult)
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
        /// 申请停工
        /// </summary>
        [HttpPost("StopWork")]
        public async Task<ResponseMessage> StopWork(ReqStopWork req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            // 是否存在停工状态的记录
            var count = await _stopWorkRepository.ExecuteScalarAsync<int>(@$"select count(*) from T_StopWorkRecord where ProjectId = {req.ProjectId} and Status !=4");
            if (count > 0)
            {
                result.Data = "已经存在该项目的停复工记录，不能申请";
                return result;
            }
            else
            {
                var stopWorkRecord = new T_StopWorkRecord();
                stopWorkRecord.ProjectId = req.ProjectId;
                stopWorkRecord.Type = EnumStopWorkType.Stop;//停工
                stopWorkRecord.Remark = req.Remark;
                stopWorkRecord.Status = 1;//停工申请中
                stopWorkRecord.StopDays = req.StopDays;
                stopWorkRecord.PlanDate = req.PlanDate;

                await _stopWorkRepository.AddAsync(stopWorkRecord);
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 申请复工
        /// </summary>
        [HttpPost("startWork")]
        public async Task<ResponseMessage> StartWork(ReqStartWork req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 必须在停工审批完成后，才能申请复工
            var count = await _stopWorkRepository.ExecuteScalarAsync<int>(@$"select count(*) from T_StopWorkRecord where ProjectId = {req.ProjectId} and Status = 2");
            if (count == 0)
            {
                result.Data = "已经存在该项目的停复工记录，不能申请";
                return result;
            }

            var record = new T_StopWorkRecord();
            record.ProjectId = req.ProjectId;
            record.Status = 3;
            record.PlanDate = req.PlanDate;
            record.Type = EnumStopWorkType.Start; //复工

            await _stopWorkRepository.AddAsync(record);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 删除停复工
        /// </summary>
        [HttpPost("DeleteStopWorkRecord")]
        public async Task<ResponseMessage> DeleteStopWorkRecord(ReqDeleteStopWorkRecord req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _stopWorkRepository.DeleteChange(req.Ids);
                if (deleteResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            return result;
        }
    }
}
