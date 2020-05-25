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
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Dtos.Request.Disclosure;
using LhsApi.Dtos.Request.Quality;
using LhsAPI.Dtos.Response.Disclosure;
using OfficeOpenXml;
using Lhs.Common;
using LhsApi.Dtos.Response;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 质检相关的controller
    /// </summary>
    [Route("api/quality")]
    //[Authorize]
    [ApiController]
    public class QualityController : PlatformControllerBase
    {
        private readonly IProjectQualityRecordRepository _qualityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public QualityController(
            IProjectQualityRecordRepository qualityRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository
            )
        {
            _qualityRepository = qualityRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        /// <summary>
        ///  监理新增质检记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addQualityRecord")]
        [AllowAnonymous]
        public async Task<ResponseMessage> AddQualityRecord(ReqAddQualityRecord request)
        {
            var record = new T_ProjectQualityRecord();
            record.UserId = request.UserId;
            record.ProjectId = request.ProjectId;
            record.QualityNo = "ZJGL" + DateTime.Now.ToString("yyyyMMddHHmmss");
            record.RectifyDate = request.RectDate;
            record.Remark = request.Note;
            record.Status = EnumProjectQualityRecordStatus.ConstructionManagerApproving;
            record.ApprovalResult = EnumApprovalResult.NoAction;

            // 处罚的具体条目，获取分类和金额
            foreach (int itemId in request.ItemIds)
            {
                // 拼接Id，用英文逗号隔开
                record.QualityItems = record.QualityItems + itemId.ToString() + ",";
                // 获取条目详情
                var item = await _qualityRepository.GetQualityItemById(itemId);
                record.StandardAmmount = record.StandardAmmount + item.Amount;
                // 质检一级分类名称和Id
                var rootCategory = await _qualityRepository.GetRootCategoryByItemId(itemId);
                record.CategoryId = rootCategory.Id;
                record.CategoryName = rootCategory.Name;
            }

            // 去掉最后一个逗号
            if (!string.IsNullOrWhiteSpace(record.QualityItems))
            {
                record.QualityItems = record.QualityItems.Substring(0, record.QualityItems.Length - 1);
            }
            //record.PunishedAmmount = record.StandardAmmount;

            // 添加图片到审批记录表
            var approval = new T_ProjectQualityApprovalRecord();
            approval.Imgs = request.Images;
            approval.Remark = request.Note;
            approval.UserId = request.UserId;
            approval.Note = "监理新增质检记录";
            await _qualityRepository.AddQualityRecordWhenFirst(record, approval);

            var result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Success,
                ErrMsg = CommonMessage.OperateSuccess,
                Data = ""
            };
            return result;
        }

        /// <summary>
        /// 后台工程部长审批（分页）
        /// </summary>
        [HttpGet("GetQualityRecordListForAdmin")]
        public async Task<ResponseMessage> GetQualityRecordListForAdmin([FromQuery] ReqGetQualityRecordListForAdmin req)
        {
            var response = new RespBasePage();
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

            var dataList = await _qualityRepository.GetPagedProjectQualityRecordList(req.UserId, req.Status, req.PageNum, req.minDate, req.maxDate, req.SearchKey);
            response.DataList = dataList.Items;
            response.TotalCount = (int) dataList.TotalCount;
            response.TotalPage = (int) dataList.TotalPages;
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList;
            return result;
        }

        /// <summary>
        /// 质检管理记录
        /// </summary>
        [HttpGet("QualityRecordList")]
        public async Task<ResponseMessage> QualityRecordList([FromQuery]ReqGetQualityRecordList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var project = await _projectRepository.SingleAsync(req.ProjectId);
            
            var supervisorName = (await GetSupervisorUser(_projectRepository, req.ProjectId)).UserName;
            var managerName = (await GetManagerUser(_projectRepository, req.ProjectId)).UserName;
            var dataListInDb = await _qualityRepository.GetProjectQualityRecordListByProjectId(req.ProjectId);
            var dataList = new List<RespGetQualityRecordList>();
            foreach (var record in dataListInDb)
            {
                var item = new RespGetQualityRecordList(record, project, managerName, supervisorName);
                dataList.Add(item);
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList;
            return result;
        }

        /// <summary>
        /// 质检审批记录
        /// </summary>
        [HttpGet("ApprovedList")]
        public async Task<ResponseMessage> ApprovedList([FromQuery]ReqGetQualityApprovedList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var dataListInDb = await _qualityRepository.GetApprovedList(req.QualityRecordId);
            var dataList = new List<RespGetQualityApprovedList>();
            foreach (T_ProjectQualityApprovalRecord qualityApprovalRecord in dataListInDb)
            {
                var item = new RespGetQualityApprovedList(qualityApprovalRecord);
                item.UserName = (await GetUser(_projectRepository, req.ProjectId, item.UserId)).RoleAndName;

                dataList.Add(item);
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = dataList;
            return result;
        }

        /// <summary>
        /// 质检详情
        /// </summary>
        [HttpGet("QualityDetail")]
        public async Task<ResponseMessage> QualityDetail([FromQuery]ReqGetQualityRecordDetailById req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var record = await _qualityRepository.SingleAsync(req.QualityRecordId);
            var projectManager = await this.GetManagerUser(_projectRepository, record.ProjectId);
            var data = new RespGetQualityRecordDetailByRecordId(record, projectManager);
            // 获取所有的处罚条目
            var itemList = record.QualityItems.Split(',');
            foreach (string item in itemList)
            {
                // 取出条目的内容
                var qualityItemFromDb = await _qualityRepository.GetQualityItemById(Convert.ToInt32(item));
                var qualityItem = new QualityItem();
                qualityItem.Name = qualityItemFromDb.Name;
                qualityItem.StandardAmmount = qualityItemFromDb.Amount;
                data.QualityItemContent.Add(qualityItem);
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 审批
        /// 质检管理状态
        /// 监理新增→工长确认（完成-罚款）
        /// 监理新增→工长不确认→工程部长确认（完成-罚款）
        /// 监理新增→工长不确认→工程部长不确认（完成-不罚款）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Approve")]
        [AllowAnonymous]
        public async Task<ResponseMessage> Approve(ReqApprove request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var record = await _qualityRepository.SingleAsync(request.Id);

            if (record.Status == EnumProjectQualityRecordStatus.Finished)
            {
                result.ErrMsg = "已经审批完成，不可重复审批";
                return result;
            }
            if (record.Status == EnumProjectQualityRecordStatus.ProjectManagerApproving && request.UserType == 1)
            {
                result.ErrMsg = "工长已经审批完成，不可重复审批";
                return result;
            }
            if (record.Status == EnumProjectQualityRecordStatus.ConstructionManagerApproving && request.UserType == 2)
            {
                result.ErrMsg = "需要工队长审批，当前用户是工程部长";
                return result;
            }

            record.EditTime = DateTime.Now;

            var approval = new T_ProjectQualityApprovalRecord();
            approval.QualityRecordId = record.Id;
            approval.QualityRecordId = record.Id;
            approval.CreateTime =DateTime.Now;
            approval.Imgs = request.Images;
            approval.Remark = request.Remark;
            approval.UserId = request.UserId;
            approval.Result = request.Approved;

            // 工长审批
            if (record.Status == EnumProjectQualityRecordStatus.ConstructionManagerApproving)
            {
                if (request.Approved)
                {
                    record.Status = EnumProjectQualityRecordStatus.Finished;
                    // 扣款
                    record.PunishedAmmount = record.StandardAmmount;
                    record.ApprovalResult = EnumApprovalResult.Approved;

                    approval.Note = "工长审批-同意-扣款-流程结束";
                }
                else
                {
                    record.Status = EnumProjectQualityRecordStatus.ProjectManagerApproving;
                    //因为等待下一步审批，所以是无状态
                    record.ApprovalResult = EnumApprovalResult.NoAction;
                    approval.Note = "工长审批-申诉，进入工程队长审批";
                }
            }
            // 工程队长审批
            else if(record.Status == EnumProjectQualityRecordStatus.ProjectManagerApproving)
            {
                record.Status = EnumProjectQualityRecordStatus.Finished;
                if (request.Approved)
                {
                    // 扣款
                    record.PunishedAmmount = record.StandardAmmount;
                    record.ApprovalResult = EnumApprovalResult.Approved;
                    approval.Note = "工程部长审批-同意-扣款-流程结束";
                }
                else
                {
                    record.ApprovalResult = EnumApprovalResult.Reject;
                    approval.Note = "工程部长审批-驳回-流程结束";
                }
            }

            await _qualityRepository.AddApproval(record, approval);
            result = new ResponseMessage()
            {
                ErrCode = MessageResultCode.Success,
                ErrMsg = CommonMessage.OperateSuccess,
                Data = ""
            };
            return result;
        }

        /// <summary>
        /// 【后台使用】删除一个或者多个质检类目内容明细（三级）
        /// </summary>
        [HttpPost("deleteQualityItem")]
        public async Task<ResponseMessage> DeleteQualityItem(ReqDeleteQualityItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                foreach (int id in req.Ids)
                {
                    await _qualityRepository.DeleteQualityItem(id);
                }

                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
                return result;
            }

            return result;
        }

        /// <summary>
        /// 【后台使用】保存一个质检类目内容明细（三级）
        /// </summary>
        [HttpPost("saveQualityItem")]
        public async Task<ResponseMessage> SaveQualityItem(ReqSaveQualityItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var item = new T_QualityItem()
            {
                Id = req.Id,
                Name = req.Name,
                CategoryId = req.CategoryId,
                Amount = req.Amount,
                EditTime = DateTime.Now,
                IsDel = false,
                CreateTime = DateTime.Now
            };
            //编辑
            if (req.Id > 0)
            {
                var itemInDb = await _qualityRepository.GetQualityItemById(req.Id);
                item.CreateTime = itemInDb.CreateTime;
                item.Id = itemInDb.Id;
                var saveResult = await _qualityRepository.SaveQualityItem(item);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }

                return result;
            }

            //新增
            await _qualityRepository.SaveQualityItem(item);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 【后台使用】删除一个或者多个质检类目分类（一级以及二级）
        /// </summary>
        [HttpPost("deleteQualityItemCategory")]
        public async Task<ResponseMessage> DeleteQualityItem(ReqDeleteQualityItemCategory req)
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
                    delResult = delResult&&(await _qualityRepository.DeleteQualityCategory(id));
                }

                if (delResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                else
                {
                    result.ErrMsg = CommonMessage.OperateFailed;
                    result.ErrCode = MessageResultCode.Error;
                    result.Data = "某些分类正在被使用，请删除类目明细";
                }
                
                return result;
            }

            return result;
        }

        /// <summary>
        /// 【后台使用】保存一个质检类目分类（一级或者二级）
        /// </summary>
        [HttpPost("saveQualityItemCategory")]
        public async Task<ResponseMessage> SaveQualityItemCategory(ReqSaveQualityItemCategory req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var item = new T_QualityItemCategory()
            {
                Id = req.Id,
                Name = req.Name,
                ParentId = req.ParentId,
                Remark = req.Remark,
                EditTime = DateTime.Now,
                IsDel = false,
                CreateTime = DateTime.Now
            };
            //编辑
            if (req.Id > 0)
            {
                var itemInDb = await _qualityRepository.GetQualityItemCategoryById(req.Id);
                item.CreateTime = itemInDb.CreateTime;
                item.Id = itemInDb.Id;
                var saveResult = await _qualityRepository.SaveQualityItemCategory(item);
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
                await _qualityRepository.SaveQualityItemCategory(item);
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
                return result;
            }
        }

        /// <summary>
        /// 【后台使用】获取质检类目分类列表（一级、二级）
        /// 如果查询一级分类列表，ParentId=0
        /// </summary>
        [HttpGet("QualityItemCategoryList")]
        public async Task<ResponseMessage> QualityItemCategoryList([FromQuery]ReqGetQualityItemCategoryList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = await _qualityRepository.GetQualityItemCategoryList(req.ParentId, req.Search);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = data;
            return result;
        }

        /// <summary>
        /// 【后台使用】获取质检类目明细列表
        /// </summary>
        [HttpGet("QualityItemList")]
        public async Task<ResponseMessage> QualityItemList([FromQuery]ReqGetQualityItemList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = await _qualityRepository.GetQualityItemListByCategoryId(req.ParentId);
            if (!string.IsNullOrEmpty( req.Search))
            {
                data = data.Where(d => d.Name.Contains(req.Search)).ToList();
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = data;
            return result;
        }
    }
}
