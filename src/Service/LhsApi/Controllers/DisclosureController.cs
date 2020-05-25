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
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Dtos.Request.Disclosure;
using LhsAPI.Dtos.Response.Disclosure;
using Swashbuckle.AspNetCore.Filters.Extensions;

namespace LhsAPI.Controllers
{
    /// <summary>
    /// 交底管理
    /// </summary>
    [Route("api/[controller]")]
    [AuthFilter]
    [ApiController]
    public class DisclosureController : PlatformControllerBase
    {
        private readonly IDisclosureTemplateRepository _disclosureRepository;
        private readonly IDisclosureItemRepository _disclosureItemRepository;
        private readonly IDisclosureTemplateItemRepository _disclosureTemplateItemRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DisclosureController(IDisclosureTemplateRepository disclosureRepository, IDisclosureItemRepository disclosureItemRepository, IDisclosureTemplateItemRepository disclosureTemplateItemRepository)
        {
            _disclosureRepository = disclosureRepository;
            _disclosureItemRepository = disclosureItemRepository;
            _disclosureTemplateItemRepository = disclosureTemplateItemRepository;
        }

        /// <summary>
        /// 获取预交底或者交底模板列表
        /// </summary>
        [HttpGet("getDisclosureTemplate")]
        public async Task<ResponseMessage> GetDisclosureTemplate([FromQuery]ReqDisclosureTemplate req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取模板列表
            var templateList = await _disclosureRepository.GetTemplateList(req.Type - 1, req.Name);
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = templateList.Select(n => new RespTemplate(n)).ToList();
            return result;
        }

        /// <summary>
        /// 获取项目的基础数据（ProjectId＞0）、模板的基础数据（ProjectId=0、TemplateId＞0）、所有的基础数据（ProjectId=0、TemplateId=0）
        /// </summary>
        [HttpGet("getProjectDisclosureBasicInfo")]
        public async Task<ResponseMessage> GetProjectDisclosureBasicInfo([FromQuery]ReqGetProjectDisclosureBasicInfo req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var respInfo = new List<RespDisclosureItem>();
            if (req.ProjectId > 0)//项目下的验收项目
            {
                var projectItemList = await _disclosureRepository.GetProjectItemList(req.Type - 1, req.ProjectId);
                respInfo = projectItemList.Select(n => new RespDisclosureItem(n)).ToList();
            }
            else if (req.TemplateId > 0)//模板下的验收项目
            {
                var itemList = await _disclosureRepository.GetTemplateItemList(req.Type - 1, req.TemplateId);
                respInfo = itemList.Select(n => new RespDisclosureItem(n)).ToList();
            }
            else //所有基础验收项目
            {
                var itemList = await _disclosureItemRepository.GetDisclosureItemList(req.Type - 1, req.PreDisclosure);
                respInfo = itemList.Select(n => new RespDisclosureItem(n)).ToList();
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = respInfo;
            return result;
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        [HttpPost("saveDisclosureTemplate")]
        public async Task<ResponseMessage> SaveDisclosureTemplate(ReqSaveDisclosure req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var template = new T_DisclosureTemplate()
            {
                Id = req.Id,
                Name = req.Name,
                Remark = req.Remark,
                EditTime = DateTime.Now,
                Type = req.Type - 1 > 0,
                IsDel = false,
                CreateTime = DateTime.Now
            };
            //编辑
            if (req.Id > 0)
            {
                var dbTemplate = await _disclosureRepository.SingleAsync(req.Id);
                template.CreateTime = dbTemplate.CreateTime;
                var saveResult = await _disclosureRepository.UpdateAsync(template);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _disclosureRepository.AddAsync(template);
            if (template.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        [HttpPost("deleteDisclosureTemplate")]
        public async Task<ResponseMessage> DeleteDisclosureTemplate(ReqDeleteDisclosure req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Ids.Any())
            {
                var deleteResult = await _disclosureRepository.DeleteTemplate(req.Ids, req.Type);
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
        /// 保存交底基础数据
        /// </summary>
        [HttpPost("saveDisclosureBasicInfo")]
        public async Task<ResponseMessage> SaveDisclosureBasicInfo(ReqSaveDisclosure req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var template = new T_DisclosureItem()
            {
                Id = req.Id,
                Name = req.Name,
                Remark = req.Remark,
                EditTime = DateTime.Now,
                Type = req.Type - 1 > 0,
                IsDel = false,
                CreateTime = DateTime.Now
            };
            //编辑
            if (req.Id > 0)
            {
                var dbTemplate = await _disclosureRepository.SingleAsync(req.Id);
                template.CreateTime = dbTemplate.CreateTime;
                var saveResult = await _disclosureItemRepository.UpdateAsync(template);
                if (saveResult)
                {
                    result.ErrMsg = CommonMessage.OperateSuccess;
                    result.ErrCode = MessageResultCode.Success;
                    result.Data = "";
                }
                return result;
            }
            //新增
            await _disclosureItemRepository.AddAsync(template);
            if (template.Id > 0)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
            }
            return result;
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        [HttpPost("deleteDisclosureBasicInfo")]
        public async Task<ResponseMessage> DeleteDisclosureBasicInfo(ReqDeleteDisclosureBasic req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var deleteResult = false;
            if (req.Ids.Any())
            {
                if (req.ProjectId <= 0)
                {
                    deleteResult = await _disclosureItemRepository.DeleteDisclosureItem(req.Ids, req.Type);
                }
                else
                {
                    deleteResult = await _disclosureItemRepository.DeleteProjectDisclosureItem(req.Ids, req.Type, req.ProjectId);
                }
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
        /// 保存交底基础数据
        /// </summary>
        [HttpPost("editDisclosureTemplateBasicItem")]
        public async Task<ResponseMessage> EditDisclosureTemplateBasicItem(ReqEditDisclosureTemplateBasicItem req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (req.Id <= 0)
            {
                return result;
            }
            var dbItems = await _disclosureTemplateItemRepository.GetTemplateItemIds(req.Id);
            var dbItemIds = dbItems.Select(n => n.ItemId).ToList();
            var toDeleteItemIds = dbItemIds.Except(req.PreDisclosureBasicIds).ToList();
            var toAddItemIds = req.PreDisclosureBasicIds.Except(dbItemIds).ToList();
            if (toDeleteItemIds.Any())
            {
                var itemList = dbItems.Where(n => toDeleteItemIds.Contains(n.ItemId)).ToList();
                itemList.ForEach(r =>
                    {
                        r.IsDel = true;
                        r.EditTime = DateTime.Now;
                    });
                var deleteResult = await _disclosureTemplateItemRepository.UpdateListAsync(itemList);
                if (!deleteResult)
                {
                    return result;
                }
            }
            if (toAddItemIds.Any())
            {
                var itemList = toAddItemIds.Select(n => new T_DisclosureTemplateItem()
                {
                    TemplateId = req.Id,
                    ItemId = n,
                    IsDel = false,
                    EditTime = DateTime.Now,
                    CreateTime = DateTime.Now
                }).ToList();
                var addResult = await _disclosureTemplateItemRepository.AddListAsync(itemList) > 0;
                if (!addResult)
                {
                    return result;
                }
            }
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }
    }
}
