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
using LhsApi.Dtos.Request;
using LhsAPI.Dtos.Request.Construction;
using LhsAPI.Dtos.Request.Disclosure;
using LhsApi.Dtos.Request.Material;
using LhsApi.Dtos.Request.Quality;
using LhsApi.Dtos.Response;
using LhsAPI.Dtos.Response.Construction;
using LhsAPI.Dtos.Response.Disclosure;
using Swashbuckle.AspNetCore.Filters.Extensions;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 施工管理
    /// </summary>
    [Route("api/[controller]")]
    [AuthFilter]
    [ApiController]
    public class ConstructionController : PlatformControllerBase
    {
        private readonly IConstructionRepository _constructionRepository;
        private readonly IProjectWorkerRepository _projectWorkerRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConstructionManageCheckRecordRepository _checkRecordRepository;
        private readonly IConstructionTemplateRepository _constructionTemplateRepository;
        private readonly ILocalMaterialItemRepository _localMaterialItemRepository;
        private readonly IMainMaterialCategoryRepository _mainMaterialCategoryRepository;
        private readonly IMainMaterialItemRepository _mainMaterialItemRepository;
        private readonly IConstructionManageTemplateRepository _manageTemplateRepository;
        private readonly IConstructionManageTemplateItemRepository _manageTemplateItemRepository;
        private readonly IConstructionManageCategoryRepository _manageCategoryRepository;
        private readonly IConstructionManageCheckTaskRepository _manageTaskRepository;
        private readonly IConstructionManageCheckStandardRepository _manageCheckStandardRepository;
        private readonly IProjectConstructionRepository _projectManageRepository;
        private readonly IProjectConstructionCheckTaskRepository _projectManageTaskRepository;
        private readonly IProjectConstructionCheckStandardRepository _projectManageStandardRepository;
        private readonly IConstructionPlanTemplateRepository _planTemplateRepository;
        private readonly IConstructionPlanTemplateItemRepository _planTemplateItemRepository;
        private readonly IConstructionPlanItemRepository _planItemRepository;
        private readonly IProjectConstructionPlanRepository _projectPlanRepository;
        private readonly IProjectPlanStageRepository _projectPlanStageRepository;
        private readonly IConstructionPlanStageRepository _planStageRepository;
        private readonly IProjectAssignTaskRepository _projectAssignTaskRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConstructionController(IConstructionRepository disConstructionRepository,
            IProjectWorkerRepository projectWorkerRepository, IWorkerRepository workerRepository,
            IUserRepository userRepository, IConstructionManageCheckRecordRepository checkRecordRepository,
            IConstructionTemplateRepository constructionTemplateRepository,
            ILocalMaterialItemRepository localMaterialItemRepository,
            IMainMaterialCategoryRepository mainMaterialCategoryRepository,
            IMainMaterialItemRepository mainMaterialItemRepository,
            IConstructionManageTemplateRepository manageTemplateRepository,
            IConstructionManageTemplateItemRepository manageTemplateItemRepository,
            IConstructionManageCategoryRepository manageCategoryRepository,
            IConstructionManageCheckTaskRepository manageTaskRepository,
            IConstructionManageCheckStandardRepository manageCheckStandardRepository,
            IProjectConstructionRepository projectManageRepository,
            IProjectConstructionCheckTaskRepository projectManageTaskRepository,
            IProjectConstructionCheckStandardRepository projectManageStandardRepository,
            IConstructionPlanTemplateRepository planTemplateRepository,
            IConstructionPlanTemplateItemRepository planTemplateItemRepository,
            IConstructionPlanItemRepository planItemRepository,
            IProjectConstructionPlanRepository projectPlanRepository,
            IProjectPlanStageRepository projectPlanStageRepository,
            IConstructionPlanStageRepository planStageRepository,
            IProjectAssignTaskRepository projectAssignTaskRepository
            )
        {
            _constructionRepository = disConstructionRepository;
            _projectWorkerRepository = projectWorkerRepository;
            _workerRepository = workerRepository;
            _userRepository = userRepository;
            _checkRecordRepository = checkRecordRepository;
            _constructionTemplateRepository = constructionTemplateRepository;
            _planTemplateRepository = planTemplateRepository;
            _localMaterialItemRepository = localMaterialItemRepository;
            _mainMaterialCategoryRepository = mainMaterialCategoryRepository;
            _mainMaterialItemRepository = mainMaterialItemRepository;
            _manageTemplateRepository = manageTemplateRepository;
            _manageTemplateItemRepository = manageTemplateItemRepository;
            _manageCategoryRepository = manageCategoryRepository;
            _manageTaskRepository = manageTaskRepository;
            _manageCheckStandardRepository = manageCheckStandardRepository;
            _projectManageRepository = projectManageRepository;
            _projectManageTaskRepository = projectManageTaskRepository;
            _projectManageStandardRepository = projectManageStandardRepository;
            _planTemplateItemRepository = planTemplateItemRepository;
            _planItemRepository = planItemRepository;
            _projectPlanRepository = projectPlanRepository;
            _projectPlanStageRepository = projectPlanStageRepository;
            _planStageRepository = planStageRepository;
            _projectAssignTaskRepository = projectAssignTaskRepository;
        }

        /// <summary>
        /// 获取模板列表-后台
        /// </summary>
        [HttpGet("getTemplateList")]
        public async Task<ResponseMessage> GetTemplateList([FromQuery]ReqTemplate req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (!Enum.IsDefined(typeof(ConstructionEnum.ConstructionTemplateType), req.Type))
            {
                return result;
            }
            // 获取模板列表
            var templateList = await _constructionTemplateRepository.GetTemplateList(req.Type, req.Name);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = templateList.Select(n => new RespTemplate(n)).ToList();
            return result;
        }

        /// <summary>
        /// 新增/编辑施工模板-后台
        /// </summary>
        [HttpPost("saveTemplate")]
        public async Task<ResponseMessage> SaveTemplate(ReqSaveTemplate req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.Type == (int)ConstructionEnum.ConstructionTemplateType.ConstructionManage)
            {
                var template = new T_ConstructionManageTemplate()
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
                    var dbTemplate = await _constructionTemplateRepository.SingleAsync(req.Id);
                    template.CreateTime = dbTemplate.CreateTime;
                    var saveResult = await _constructionTemplateRepository.UpdateAsync(template);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _constructionTemplateRepository.AddAsync(template);
                if (template.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else if (req.Type == (int)ConstructionEnum.ConstructionTemplateType.ConstructionPlan)
            {
                var template = new T_ConstructionPlanTemplate()
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
                    var dbTemplate = await _planTemplateRepository.SingleAsync(req.Id);
                    template.CreateTime = dbTemplate.CreateTime;
                    var saveResult = await _planTemplateRepository.UpdateAsync(template);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _planTemplateRepository.AddAsync(template);
                if (template.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工管理模板-保存模板的基础项-后台
        /// </summary>
        [HttpPost("manageTemplate/saveTemplateItems")]
        public async Task<ResponseMessage> SaveTemplateItems(ReqSaveTemplateItems req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.TemplateId <= 0)
            {
                return result;
            }
            var allList = await _manageTemplateItemRepository.GetTemplateItemList(req.TemplateId);
            var toDeleteItems = allList.Where(n => !req.ItemIds.Contains(n.CategoryId)).ToList();
            if (toDeleteItems.Any())
            {
                toDeleteItems.ForEach(n =>
                {
                    n.IsDel = true;
                    n.EditTime = DateTime.Now;
                });
                var deleteResult = await _manageTemplateItemRepository.UpdateListAsync(toDeleteItems);
                if (!deleteResult)
                {
                    return result;
                }
            }
            //新增
            var dbIds = allList.Select(n => n.CategoryId).Distinct().ToList();
            var toAddIds = req.ItemIds.Except(dbIds).ToList();
            if (toAddIds.Any())
            {
                var toAddItems = toAddIds.Select(n => new T_ConstructionManageTemplateItem()
                {
                    TemplateId = req.TemplateId,
                    CategoryId = n
                }).ToList();

                var addResult = await _manageTemplateItemRepository.AddListAsync(toAddItems);
                if (addResult <= 0)
                {
                    return result;
                }
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";

            return result;
        }

        /// <summary>
        /// 删除模板-后台
        /// </summary>
        [HttpPost("deleteTemplate")]
        public async Task<ResponseMessage> DeleteTemplate(ReqDeleteTemplate req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.Type == (int)ConstructionEnum.ConstructionTemplateType.ConstructionManage || req.Type == (int)ConstructionEnum.ConstructionTemplateType.ConstructionPlan)
            {
                if (req.Ids.Any())
                {
                    var deleteResult = await _constructionRepository.DeleteTemplate(req.Ids, req.Type);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 施工管理-获取模板类目列表-后台使用
        /// </summary>
        [HttpGet("manageTemplate/itemList")]
        public async Task<ResponseMessage> ItemList([FromQuery]ReqManageList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var respInfo = new List<RespManageList>();
            if (req.ProjectId > 0)//项目下的基础数据
            {
                var projectManageInfo = await _projectManageRepository.GetProjectConstructionManageList(req.ProjectId);
                respInfo = projectManageInfo.GroupBy(n => new { n.CategoryId, n.CategoryName })
                    .Select(r =>
                    {
                        return new RespManageList()
                        {
                            ItemId = r.Key.CategoryId,
                            ItemName = r.Key.CategoryName,
                            Selected = true,
                            TaskList = r.Where(p => p.TaskId > 0).GroupBy(task => new { task.TaskId, task.TaskName })
                                .Select(task => new ManageTask()
                                {
                                    TaskId = task.Key.TaskId,
                                    TaskName = task.Key.TaskName
                                }).ToList().OrderBy(task => task.TaskId).ToList()
                        };
                    }).OrderBy(r => r.ItemId).ToList();
            }
            else if (req.TemplateId > 0)//模板下的基础数据
            {
                var templateItems = await _manageTemplateItemRepository.GetTemplateItemList(req.TemplateId);
                var items = await _manageCategoryRepository.GetList();
                var tasks = await _manageTaskRepository.GetList();
                respInfo = items.Select(n => new RespManageList(n, templateItems)
                {
                    TaskList = tasks.Where(r => r.CategoryId == n.Id)
                        .Select(y => new ManageTask(y)).ToList()
                }).ToList();
            }
            else //所有基础数据
            {
                var items = await _manageCategoryRepository.GetList();
                var tasks = await _manageTaskRepository.GetList();
                respInfo = items.Select(n => new RespManageList(n)
                {
                    TaskList = tasks.Where(r => r.CategoryId == n.Id)
                        .Select(y => new ManageTask(y)).ToList()
                }).ToList();
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 施工管理-新增/编辑类目-后台
        /// </summary>
        [HttpPost("manageTemplate/saveItem")]
        public async Task<ResponseMessage> SaveItem(ReqSaveItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)
            {
                var data = new T_ConstructionManageCategory()
                {
                    Id = req.ItemId,
                    Name = req.ItemName
                };
                //编辑
                if (req.ItemId > 0)
                {
                    var dbData = await _manageCategoryRepository.SingleAsync(req.ItemId);
                    data.CreateTime = dbData.CreateTime;
                    var saveResult = await _manageCategoryRepository.UpdateAsync(data);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _manageCategoryRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else
            {
                //编辑
                if (req.ItemId > 0)
                {
                    var dbData = await _projectManageRepository.SingleAsync(req.ItemId);
                    dbData.CategoryName = req.ItemName;
                    dbData.EditTime = DateTime.Now;
                    var saveResult = await _projectManageRepository.UpdateAsync(dbData);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                var data = new T_ProjectConstructionManage()
                {
                    ProjectId = req.ProjectId,
                    CategoryId = 0,
                    CategoryName = req.ItemName
                };
                await _projectManageRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工管理-删除类目-后台
        /// </summary>
        [HttpPost("manageTemplate/deleteItems")]
        public async Task<ResponseMessage> DeleteItems(ReqDeleteItems req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)//基础数据中删除
            {
                if (req.ItemIds.Any())
                {
                    var deleteResult = await _manageCategoryRepository.DeleteBasicItem(req.ItemIds);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
            }
            else//项目中删除
            {
                if (req.ItemIds.Any())
                {
                    var deleteResult = await _projectManageRepository.DeleteBasicItem(req.ItemIds);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 施工管理-新增/编辑任务详情-后台
        /// </summary>
        [HttpPost("manageTemplate/saveItemTask")]
        public async Task<ResponseMessage> SaveItemTask(ReqSaveItemTask req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ItemId <= 0)
            {
                return result;
            }
            if (req.ProjectId <= 0)
            {
                var data = new T_ConstructionManageCheckTask()
                {
                    Id = req.TaskId,
                    Name = req.TaskName,
                    CategoryId = req.ItemId
                };
                //编辑
                if (req.TaskId > 0)
                {
                    var dbData = await _manageTaskRepository.SingleAsync(req.TaskId);
                    data.CreateTime = dbData.CreateTime;
                    var saveResult = await _manageTaskRepository.UpdateAsync(data);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _manageTaskRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else
            {
                //编辑
                if (req.TaskId > 0)
                {
                    var dbData = await _projectManageTaskRepository.SingleAsync(req.TaskId);
                    dbData.ManageTaskName = req.TaskName;
                    dbData.EditTime = DateTime.Now;
                    var saveResult = await _projectManageTaskRepository.UpdateAsync(dbData);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                var data = new T_ProjectConstructionCheckTask()
                {
                    ProjectManageId = req.ItemId,
                    ManageTaskId = 0,
                    ManageTaskName = req.TaskName,
                    TaskNo = $"{CommonConst.NoForProjectManageTask}{CommonHelper.GetTimeStamp()}"
                };
                await _projectManageTaskRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工管理-删除任务-后台
        /// </summary>
        [HttpPost("manageTemplate/deleteItemTasks")]
        public async Task<ResponseMessage> DeleteItemTasks(ReqDeleteItemTasks req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ItemId <= 0 || req.TaskId <= 0)
            {
                return result;
            }
            if (req.ProjectId <= 0)//基础数据中删除
            {
                var taskInfo = await _manageTaskRepository.SingleAsync(req.TaskId);
                taskInfo.IsDel = true;
                taskInfo.EditTime = DateTime.Now;
                var deleteResult = await _manageTaskRepository.UpdateAsync(taskInfo);
                if (deleteResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else//项目中删除
            {
                var taskInfo = await _projectManageTaskRepository.SingleAsync(req.TaskId);
                taskInfo.IsDel = true;
                taskInfo.EditTime = DateTime.Now;
                var deleteResult = await _projectManageTaskRepository.UpdateAsync(taskInfo);
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
        /// 施工管理-获取验收标准-后台使用
        /// </summary>
        [HttpGet("manageTemplate/getTaskCheckStandard")]
        public async Task<ResponseMessage> GetTaskCheckStandard([FromQuery]ReqGetTaskCheckStandard req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var respInfo = new List<RespStandardList>();
            if (req.ProjectId > 0)//项目下的验收标准
            {
                var projectStandardInfo = await _projectManageStandardRepository.GetList();
                if (string.IsNullOrEmpty(req.SearchName))
                {
                    respInfo = projectStandardInfo.Where(n => n.ProjectTaskId == req.TaskId).ToList()
                        .Select(r => new RespStandardList(r)).ToList();
                }
                else
                {
                    respInfo = projectStandardInfo.Where(n => n.ProjectTaskId == req.TaskId
                                                              && (n.Content.Contains(req.SearchName) || n.Name.Contains(req.SearchName))).ToList()
                        .Select(r => new RespStandardList(r)).ToList();
                }
            }
            else //基础数据中的验收标准
            {
                var basicStandardInfo = await _manageCheckStandardRepository.GetList();
                if (string.IsNullOrEmpty(req.SearchName))
                {
                    respInfo = basicStandardInfo.Where(n => n.TaskId == req.TaskId).ToList()
                        .Select(r => new RespStandardList(r)).ToList();
                }
                else
                {
                    respInfo = basicStandardInfo.Where(n => n.TaskId == req.TaskId
                                                            && (n.Content.Contains(req.SearchName) || n.Name.Contains(req.SearchName))).ToList()
                        .Select(r => new RespStandardList(r)).ToList();
                }
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 施工管理-新增/编辑验收标准-后台
        /// </summary>
        [HttpPost("manageTemplate/saveTaskCheckStandard")]
        public async Task<ResponseMessage> SaveTaskCheckStandard(ReqSaveTaskCheckStandard req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.TaskId <= 0)
            {
                return result;
            }
            if (req.ProjectId <= 0)//维护基础数据中的验收标准
            {
                var data = new T_ConstructionManageCheckStandard()
                {
                    Id = req.StandardId,
                    TaskId = req.TaskId,
                    Name = req.Name,
                    Content = req.Desc
                };
                //编辑
                if (req.StandardId > 0)
                {
                    var dbData = await _manageCheckStandardRepository.SingleAsync(req.StandardId);
                    data.CreateTime = dbData.CreateTime;
                    var saveResult = await _manageCheckStandardRepository.UpdateAsync(data);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _manageCheckStandardRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else//维护项目的验收标准
            {
                //编辑
                if (req.StandardId > 0)
                {
                    var dbData = await _projectManageStandardRepository.SingleAsync(req.StandardId);
                    dbData.Name = req.Name;
                    dbData.Content = req.Desc;
                    dbData.EditTime = DateTime.Now;
                    var saveResult = await _projectManageStandardRepository.UpdateAsync(dbData);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                var data = new T_ProjectConstructionCheckStandard()
                {
                    ProjectTaskId = req.TaskId,
                    StandardId = 0,
                    Name = req.Name,
                    Content = req.Desc
                };
                await _projectManageStandardRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工管理-删除验收标准-后台
        /// </summary>
        [HttpPost("manageTemplate/deleteTaskCheckStandard")]
        public async Task<ResponseMessage> DeleteTaskCheckStandard(ReqDeleteTaskCheckStandard req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (!req.StandardIds.Any() || req.TaskId <= 0)
            {
                return result;
            }
            bool deleteResult;
            if (req.ProjectId <= 0)//基础数据中删除
            {
                deleteResult = await _manageCheckStandardRepository.DeleteBasicItem(req.StandardIds);
            }
            else//项目中删除
            {
                deleteResult = await _projectManageStandardRepository.DeleteBasicItem(req.StandardIds);
            }
            if (deleteResult)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 施工计划-获取模板下项目列表-后台使用
        /// </summary>
        [HttpGet("planTemplate/itemList")]
        public async Task<ResponseMessage> PlanItemList([FromQuery]ReqManageList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var respInfo = new List<RespPlanList>();
            if (req.ProjectId > 0)//项目下的基础数据
            {
                var projectPlanInfo = await _projectPlanRepository.GetProjectConstructionPlanList(req.ProjectId);
                var stages = await _projectPlanStageRepository.GetProjectPlanStageList(req.ProjectId);
                respInfo = projectPlanInfo.Select(r => new RespPlanList(r, stages)).ToList();
            }
            else if (req.TemplateId > 0)//模板下的基础数据
            {
                var templateItems = await _planTemplateItemRepository.GetTemplateItemList(req.TemplateId);
                var items = await _planItemRepository.GetList();
                var stages = await _planStageRepository.GetList();
                respInfo = items.Select(n => new RespPlanList(n, templateItems, stages)).ToList();
            }
            else //所有基础数据
            {
                var items = await _planItemRepository.GetList();
                var stages = await _planStageRepository.GetList();
                respInfo = items.Select(n => new RespPlanList(n, stages)).ToList();
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 施工计划-保存模板的基础项-后台
        /// </summary>
        [HttpPost("planTemplate/saveTemplateItems")]
        public async Task<ResponseMessage> SavePlanTemplateItems(ReqSaveTemplateItems req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Success,
                Data = 0
            };
            if (req.TemplateId <= 0)
            {
                return result;
            }
            var allList = await _planTemplateItemRepository.GetTemplateItemList(req.TemplateId);
            var toDeleteItems = allList.Where(n => !req.ItemIds.Contains(n.CategoryId)).ToList();
            if (toDeleteItems.Any())
            {
                toDeleteItems.ForEach(n =>
                {
                    n.IsDel = true;
                    n.EditTime = DateTime.Now;
                });
                var deleteResult = await _planTemplateItemRepository.UpdateListAsync(toDeleteItems);
                if (!deleteResult)
                {
                    return result;
                }
            }
            //新增
            var dbIds = allList.Select(n => n.CategoryId).Distinct().ToList();
            var toAddIds = req.ItemIds.Except(dbIds).ToList();
            if (toAddIds.Any())
            {
                var toAddItems = toAddIds.Select(n => new T_ConstructionPlanTemplateItem()
                {
                    TemplateId = req.TemplateId,
                    CategoryId = n
                }).ToList();

                var addResult = await _planTemplateItemRepository.AddListAsync(toAddItems);
                if (addResult <= 0)
                {
                    return result;
                }
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";

            return result;
        }

        /// <summary>
        /// 施工计划管理-新增/编辑项目-后台
        /// </summary>
        [HttpPost("planTemplate/saveItem")]
        public async Task<ResponseMessage> SavePlanItem(ReqSavePlanItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)
            {
                var data = new T_ConstructionPlanItem()
                {
                    Id = req.ItemId,
                    Name = req.ItemName,
                    InnerDays = req.InternalControlCycle,
                    StageId = req.StageId,
                    ContractDays = req.ContractCycle
                };
                //编辑
                if (req.ItemId > 0)
                {
                    var dbData = await _planItemRepository.SingleAsync(req.ItemId);
                    data.CreateTime = dbData.CreateTime;
                    data.Contents = dbData.Contents;
                    if (req.StageId > 0)
                    {
                        var stages = await _planStageRepository.GetList();
                        data.ContractDays = stages.First(n => n.Id == req.StageId).Days;
                    }
                    var saveResult = await _planItemRepository.UpdateAsync(data);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _planItemRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else
            {
                //编辑
                if (req.ItemId > 0)
                {
                    var dbData = await _projectPlanRepository.SingleAsync(req.ItemId);
                    dbData.Name = req.ItemName;
                    dbData.Days = req.InternalControlCycle;
                    dbData.ProjectStageId = req.StageId;
                    dbData.EditTime = DateTime.Now;
                    if (req.StageId > 0)
                    {
                        var stages = await _projectPlanRepository.GetList();
                        dbData.ContractDays = stages.First(n => n.Id == req.StageId).Days;
                    }
                    var saveResult = await _projectPlanRepository.UpdateAsync(dbData);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                var data = new T_ProjectConstructionPlan()
                {
                    ProjectId = req.ProjectId,
                    Name = req.ItemName,
                    PlanItemId = 0,
                    Days = req.InternalControlCycle,
                    ProjectStageId = req.StageId,
                    ContractDays = req.ContractCycle
                };
                await _projectPlanRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工计划管理-删除项目-后台
        /// </summary>
        [HttpPost("planTemplate/deleteItems")]
        public async Task<ResponseMessage> DeletePlanItems(ReqDeleteItems req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)//基础数据中删除
            {
                if (req.ItemIds.Any())
                {
                    var deleteResult = await _planItemRepository.DeleteBasicItem(req.ItemIds);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
            }
            else//项目中删除
            {
                if (req.ItemIds.Any())
                {
                    var deleteResult = await _projectPlanRepository.DeleteBasicItem(req.ItemIds);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 施工计划管理-添加内容-后台
        /// </summary>
        [HttpPost("planTemplate/addItemContent")]
        public async Task<ResponseMessage> SavePlanItemContent(ReqSavePlanItemContent req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ItemId <= 0)
            {
                return result;
            }
            if (req.ProjectId <= 0)
            {
                var dbData = await _planItemRepository.SingleAsync(req.ItemId);
                dbData.EditTime = DateTime.Now;
                dbData.Contents = req.ContentName.Any()
                    ? string.Join(CommonConst.Separator, req.ContentName)
                    : string.Empty;
                var saveResult = await _planItemRepository.UpdateAsync(dbData);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else
            {
                var dbData = await _projectPlanRepository.SingleAsync(req.ItemId);
                dbData.EditTime = DateTime.Now;
                dbData.Contents = req.ContentName.Any()
                    ? string.Join(CommonConst.Separator, req.ContentName)
                    : string.Empty;
                var saveResult = await _projectPlanRepository.UpdateAsync(dbData);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工计划管理-获取阶段列表-后台
        /// </summary>
        [HttpPost("planTemplate/getStageList")]
        public async Task<ResponseMessage> GetStageList(ReqGetStageList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId > 0)
            {
                var stages = await _projectPlanStageRepository.GetProjectPlanStageList(req.ProjectId);
                result.Data = stages.Select(n => new RespStageList(n)).ToList();
            }
            else
            {
                var stages = await _planStageRepository.GetList();
                result.Data = stages.Select(n => new RespStageList(n)).ToList();

            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            return result;
        }

        /// <summary>
        /// 施工计划管理-新增/编辑阶段-后台
        /// </summary>
        [HttpPost("planTemplate/addOrEditStage")]
        public async Task<ResponseMessage> AddOrEditStage(ReqAddOrEditStage req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)
            {
                var data = new T_ConstructionPlanStage()
                {
                    Id = req.StageId,
                    Name = req.StageName,
                    Days = req.Cycle
                };
                //编辑
                if (req.StageId > 0)
                {
                    var dbData = await _planStageRepository.SingleAsync(req.StageId);
                    data.CreateTime = dbData.CreateTime;
                    var saveResult = await _planStageRepository.UpdateAsync(data);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                await _planStageRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            else
            {
                //编辑
                if (req.StageId > 0)
                {
                    var dbData = await _projectPlanStageRepository.SingleAsync(req.StageId);
                    dbData.Name = req.StageName;
                    dbData.Days = req.Cycle;
                    dbData.EditTime = DateTime.Now;
                    var saveResult = await _projectPlanStageRepository.UpdateAsync(dbData);
                    if (saveResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
                //新增
                var data = new T_ProjectPlanStage()
                {
                    ProjectId = req.ProjectId,
                    Name = req.StageName,
                    Days = req.Cycle
                };
                await _projectPlanStageRepository.AddAsync(data);
                if (data.Id > 0)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
            }
            return result;
        }

        /// <summary>
        /// 施工计划-删除阶段-后台
        /// </summary>
        [HttpPost("planTemplate/deleteStage")]
        public async Task<ResponseMessage> DeleteStage(ReqDeleteStages req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0)//基础数据中删除
            {
                if (req.StageIds.Any())
                {
                    var deleteResult = await _planStageRepository.DeleteBasicItem(req.StageIds);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
            }
            else//项目中删除
            {
                if (req.StageIds.Any())
                {
                    var deleteResult = await _projectPlanStageRepository.DeleteBasicItem(req.StageIds);
                    if (deleteResult)
                    {
                        result.ErrMsg = CommonMessage.OperateSuccess;
                        result.ErrCode = MessageResultCode.Success;
                        result.Data = "";
                    }
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取地采基础数据-后台
        /// </summary>
        [HttpGet("localMaterial/getBasicInfo")]
        public async Task<ResponseMessage> GetProjectDisclosureBasicInfo([FromQuery]ReqLocalMaterialBasic req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            //if (req.ProjectId > 0)//项目下的基础数据
            //{
            //    //todo：项目下可能没有基础数据，确定不用了就删除
            //}
            //else if (req.TemplateId > 0)//模板下的基础数据
            //{
            //    var itemList = await _constructionRepository.GetLocalTemplateItemList(req.TemplateId);
            //    respInfo = itemList.Select(n => new RespLocalBasicItem(n)).ToList();
            //}
            //else //所有基础数据
            //{
            var itemList = await _constructionRepository.GetLocalBasicItemList(req.Name);
            var respInfo = itemList.Select(n => new RespLocalBasicItem(n)).ToList();
            //}
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 保存地采基础数据-后台
        /// </summary>
        [HttpPost("localMaterial/save")]
        public async Task<ResponseMessage> SaveLocalBasic(ReqSaveLocalBasic req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = new T_LocalMaterialItem()
            {
                Id = req.Id,
                Name = req.Name,
                Code = req.Code.Any() ? string.Join(CommonConst.Separator, req.Code) : string.Empty
            };
            if (string.IsNullOrEmpty(req.Name) || !req.Code.Any())
            {
                result.ErrMsg = CommonMessage.MaterialCodeNotEmpty;
                return result;
            }
            //编码已存在时，提交失败
            var allData = await _localMaterialItemRepository.GetList();
            if (allData.Any(n => n.Code.Split(CommonConst.Separator).ToList().Intersect(req.Code).Any() && n.Id != req.Id))
            {
                result.ErrMsg = CommonMessage.MaterialCodeExisted;
                return result;
            }
            //编辑
            if (req.Id > 0)
            {
                var dbData = await _localMaterialItemRepository.SingleAsync(req.Id);
                data.CreateTime = dbData.CreateTime;
                var saveResult = await _localMaterialItemRepository.UpdateAsync(data);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _localMaterialItemRepository.AddAsync(data);
            if (data.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 删除地采基础数据
        /// </summary>
        [HttpPost("localMaterial/delete")]
        public async Task<ResponseMessage> DeleteLocalMaterialBasicInfo(ReqDeleteLocalBasic req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _localMaterialItemRepository.DeleteBasicItem(req.Ids);
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
        /// 主材-获取类别列表-后台使用
        /// </summary>
        [HttpGet("material/categoryList")]
        public async Task<ResponseMessage> MainMaterialCategoryList([FromQuery]ReqAuth req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var categories = await _mainMaterialCategoryRepository.GetList();
            var items = await _mainMaterialItemRepository.GetList();
            var respInfo = categories.Select(n => new MainMaterialBasicCategory(n)
            {
                MaterialList = items.Where(r => r.CategoryId == n.Id).Select(y => new MainMaterialBasic(y)).ToList()
            });
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 主材-新增/编辑类别-后台使用
        /// </summary>
        [HttpPost("material/saveCategory")]
        public async Task<ResponseMessage> SaveCategory(ReqSaveMainCategory req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = new T_MainMaterialCategory()
            {
                Id = req.Id,
                Name = req.Name
            };
            //编辑
            if (req.Id > 0)
            {
                var dbData = await _mainMaterialCategoryRepository.SingleAsync(req.Id);
                data.CreateTime = dbData.CreateTime;
                var saveResult = await _mainMaterialCategoryRepository.UpdateAsync(data);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _mainMaterialCategoryRepository.AddAsync(data);
            if (data.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 主材-删除类别-后台使用
        /// </summary>
        [HttpPost("localMaterial/deleteCategory")]
        public async Task<ResponseMessage> DeleteCategory(ReqDeleteLocalBasic req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _mainMaterialCategoryRepository.DeleteBasicItem(req.Ids);
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
        /// 主材-新增/编辑主材-后台使用
        /// </summary>
        [HttpPost("material/saveMaterial")]
        public async Task<ResponseMessage> SaveMaterial(ReqSaveMainItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var data = new T_MainMaterialItem()
            {
                Id = req.Id,
                Name = req.Name,
                CategoryId = req.CategoryId,
                Code = req.Code.Any() ? string.Join(CommonConst.Separator, req.Code) : string.Empty
            };
            if (string.IsNullOrEmpty(req.Name) || !req.Code.Any() || req.CategoryId <= 0)
            {
                result.ErrMsg = CommonMessage.MaterialCodeNotEmpty;
                return result;
            }
            //编码已存在时，提交失败
            var allData = await _mainMaterialItemRepository.GetList();
            if (allData.Any(n => n.Code.Split(CommonConst.Separator).ToList().Intersect(req.Code).Any() && n.Id != req.Id))
            {
                result.ErrMsg = CommonMessage.MaterialCodeExisted;
                return result;
            }
            //编辑
            if (req.Id > 0)
            {
                var dbData = await _mainMaterialItemRepository.SingleAsync(req.Id);
                data.CreateTime = dbData.CreateTime;
                var saveResult = await _mainMaterialItemRepository.UpdateAsync(data);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _mainMaterialItemRepository.AddAsync(data);
            if (data.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 主材-删除主材-后台使用
        /// </summary>
        [HttpPost("localMaterial/deleteMaterial")]
        public async Task<ResponseMessage> DeleteMaterial(ReqDeleteMainItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Id > 0)
            {
                var dbData = await _mainMaterialItemRepository.SingleAsync(req.Id);
                dbData.IsDel = true;
                dbData.EditTime = DateTime.Now;
                var saveResult = await _mainMaterialItemRepository.UpdateAsync(dbData);
                if (saveResult)
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
        /// 获取项目的施工管理列表-APP
        /// </summary>
        [HttpGet("list")]
        public async Task<ResponseMessage> List([FromQuery]ReqAPPConstructionManageList req)
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
            var constructionList = await _constructionRepository.GetConstructionList(req.ProjectId);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = constructionList.Select(n => new RespList(n)).ToList();
            return result;
        }

        /// <summary>
        /// 获取施工管理-派工列表-APP
        /// </summary>
        [HttpGet("assignList")]
        public async Task<ResponseMessage> AssignList([FromQuery]ReqConstructionManageList req)
        {
            var response = new RespBasePage();

            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            if (req.ProjectId <= 0 || req.WorkTypeId < 0)
            {
                return result;
            }

            var constructionList = await _constructionRepository.GetAssignWorkerList(req.WorkTypeId, req.PageNum);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            response.TotalCount = (int)constructionList.TotalCount;
            response.TotalPage = (int)constructionList.TotalPages;
            response.DataList = constructionList.Items.Select(n => new RespAssignWorkerList(n)).ToList();
            result.Data = response;
            return result;
        }

        /// <summary>
        /// 提交派工：新增工人时UserId=0-APP
        /// </summary>
        [HttpPost("Assign")]
        public async Task<ResponseMessage> Assign(ReqAssign req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectManageId <= 0)
            {
                return result;
            }
            //派工
            if (req.TargetUserId > 0)
            {
                var projectWorker = new T_ProjectWorker()
                {
                    ProjectManageId = req.ProjectManageId,
                    WorkerId = req.TargetUserId,
                    IsDel = false,
                    EditTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
                var saveResult = await _projectWorkerRepository.AddAsync(projectWorker);
                if (saveResult <= 0)
                {
                    return result;
                }
                var assignResult = await SaveAssignTask(req.ProjectManageId, req.OperatorId, req.TargetUserId);
                if (!assignResult)
                {
                    return result;
                }
            }
            else
            {
                if (req.WorkType <= 0 || string.IsNullOrEmpty(req.WorkerName))
                {
                    return result;
                }
                //新增工人并派工
                var worker = new T_Worker()
                {
                    Name = req.WorkerName,
                    Phone = req.Phone,
                    CompanyId = req.CompanyId,
                    Sex = req.Sex != "男",
                    WorkType = req.WorkType,
                    IsDel = false,
                    EditTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
                await _workerRepository.AddAsync(worker);
                if (worker.Id <= 0)
                {
                    return result;
                }
                var projectWorker = new T_ProjectWorker()
                {
                    ProjectManageId = req.ProjectManageId,
                    WorkerId = worker.Id,
                    IsDel = false,
                    EditTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };
                await _projectWorkerRepository.AddAsync(projectWorker);
                if (projectWorker.Id <= 0)
                {
                    return result;
                }
                var assignResult = await SaveAssignTask(req.ProjectManageId, req.OperatorId, req.TargetUserId);
                if (!assignResult)
                {
                    return result;
                }

            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 维护派工任务
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SaveAssignTask(int projectManageId, int operatorId, int workerId)
        {
            var projectManage = await _projectManageRepository.SingleAsync(projectManageId);
            var workerInfo = await _workerRepository.GetWorkerInfo(workerId);
            var assignTask = new T_ProjectAssignTask()
            {
                ProjectId = projectManage.ProjectId,
                WorkerId = workerInfo.Id,
                WorkerName = workerInfo.Name,
                Phone = workerInfo.Phone,
                Male = workerInfo.Sex,
                CompanyId = workerInfo.CompanyId,
                CompanyName = workerInfo.CompanyName,
                ConstructionManager = operatorId,
                TaskNo = $"{CommonConst.AssignWorkerTaskPrefix}{CommonHelper.GetTimeStamp()}",
                TaskName = CommonConst.AssignWorkerTaskName
            };
            await _projectAssignTaskRepository.AddAsync(assignTask);
            return assignTask.Id > 0;
        }

        /// <summary>
        /// 获取施工管理-验收列表-APP
        /// </summary>
        [HttpGet("checkList")]
        public async Task<ResponseMessage> CheckList([FromQuery]ReqConstructionManageList req)
        {
            var response = new RespBasePage();

            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = response
            };
            if (req.ProjectId <= 0 || req.WorkTypeId < 0)
            {
                return result;
            }

            var constructionList = await _constructionRepository.GetCheckList(req.WorkTypeId, req.PageNum);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            response.TotalCount = (int)constructionList.TotalCount;
            response.TotalPage = (int)constructionList.TotalPages;
            response.DataList = constructionList.Items.Select(n => new RespCheckList(n)).ToList();
            result.Data = response;
            return result;
        }

        /// <summary>
        /// 获取验收详情-APP
        /// </summary>
        [HttpGet("checkDetailByCheckId")]
        public async Task<ResponseMessage> CheckDetailByCheckId([FromQuery]ReqCheckDetailByCheckId req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.CheckId <= 0)
            {
                return result;
            }
            var checkTask = await _constructionRepository.GetCheckTask(req.CheckId);
            var checkRecord = await _constructionRepository.GetCheckRecordList(req.CheckId);
            var respCheckRecord = new List<CheckRecord>();
            if (checkRecord.Any())
            {
                var userIds = checkRecord.Select(n => n.UserId).Distinct().ToList();
                var userInfos = await _userRepository.GetUserInfo(userIds);
                respCheckRecord = checkRecord.Select(n =>
                    new CheckRecord(n, userInfos.FirstOrDefault(r => r.Id == n.UserId))).ToList();
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = new RespCheckDetail(checkTask)
            {
                CheckRecordList = respCheckRecord
            };
            return result;
        }

        /// <summary>
        /// 提交验收-APP
        /// </summary>
        [HttpPost("check")]
        public async Task<ResponseMessage> Check(ReqCheck req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.ProjectId <= 0 || req.CheckId <= 0)
            {
                return result;
            }
            var checkRecord = new T_ProjectConstructionCheckRecord()
            {
                UserId = req.ApproveUserId,
                ProjectTaskId = req.CheckId,
                Result = req.Approve,
                Remark = req.Note,
                Imgs = req.Images.Any() ? string.Join(CommonConst.Separator, req.Images) : string.Empty,
                Location = req.Address,
                CreateTime = DateTime.Now
            };
            await _checkRecordRepository.AddAsync(checkRecord);
            if (checkRecord.Id <= 0)
            {
                return result;
            }
            var updateResult = await _constructionRepository.SaveCheckTaskStatus(req.CheckId);
            if (!updateResult)
            {
                return result;
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

    }
}
