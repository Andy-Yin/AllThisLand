using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Lhs.Common;
using Lhs.Interface;
using LhsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.U9;
using Lhs.Helper;
using Lhs.Service;
using LhsAPI;
using LhsApi.Dtos.Request.Material;
using LhsApi.Dtos.Request.Quality;
using LhsApi.Dtos.Response.Material;
using LhsApi.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using OfficeOpenXml.Drawing.Chart;
using U9Service;
using MeasureTaskItem = LhsApi.Dtos.Request.Material.MeasureTaskItem;

namespace LhsApi.Controllers
{
    /// <summary>
    /// 主材相关
    /// </summary>
    [Route("api/Material")]
    //[Authorize]
    [ApiController]
    public class MaterialController : PlatformControllerBase
    {
        private readonly IProjectMaterialRepository _projectMaterialRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectMeasureTaskRepository _projectMeasureTaskRepository;
        private readonly IProjectOrderTaskRepository _projectOrderTaskRepository;
        private readonly IProjectInstallTaskRepository _projectInstallTaskRepository;
        private readonly IProjectMeasureTaskItemRepository _projectMeasureTaskItemRepository;
        private readonly IProjectPickMaterialRepository _projectPickMaterialRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMainMaterialItemRepository _mainMaterialItemRepository;
        private readonly ILocalMaterialItemRepository _localMaterialItemRepository;
        private readonly IMainMaterialCategoryRepository _mainMaterialCategoryRepository;
        private readonly IProjectMaterialItemLogRepository _projectMaterialItemLogRepository;
        private readonly IWorkTypeRepository _workTypeRepository;
        private readonly IProjectMaterialLabourRepository _projectMaterialLabourRepository;

        /// <summary>
        /// 构造函数，注入
        /// </summary>
        public MaterialController(
            IProjectMaterialRepository materialRepository,
            IProjectMeasureTaskRepository measureTaskRepository,
            IUserRepository userRepository,
            IProjectOrderTaskRepository orderTaskRepository,
            IProjectInstallTaskRepository installTaskRepository,
            IProjectMeasureTaskItemRepository measureTaskItemRepository,
            IProjectPickMaterialRepository projectPickMaterialRepository,
            IProjectRepository projectRepository,
            IMainMaterialItemRepository mainMaterialItemRepository,
            ILocalMaterialItemRepository localMaterialItemRepository,
            IMainMaterialCategoryRepository mainMaterialCategoryRepository,
            IProjectMaterialItemLogRepository projectMaterialItemLogRepository,
            IWorkTypeRepository workTypeRepository,
            IProjectMaterialLabourRepository projectMaterialLabourRepository
        )
        {
            _projectMaterialRepository = materialRepository;
            _projectMeasureTaskRepository = measureTaskRepository;
            _userRepository = userRepository;
            _projectOrderTaskRepository = orderTaskRepository;
            _projectInstallTaskRepository = installTaskRepository;
            _projectMeasureTaskItemRepository = measureTaskItemRepository;
            _projectPickMaterialRepository = projectPickMaterialRepository;
            _projectRepository = projectRepository;
            _mainMaterialItemRepository = mainMaterialItemRepository;
            _localMaterialItemRepository = localMaterialItemRepository;
            _mainMaterialCategoryRepository = mainMaterialCategoryRepository;
            _projectMaterialItemLogRepository = projectMaterialItemLogRepository;
            _workTypeRepository = workTypeRepository;
            _projectMaterialLabourRepository = projectMaterialLabourRepository;
        }

        /// <summary>
        /// 【U9使用】批量同步所有的物料，如果有一个物料有问题，整体不可提交
        /// </summary>
        [HttpPost("BatchSyncMaterialFromU9")]
        public async Task<ResponseMessage> BatchSyncMaterialFromU9(ReqBatchSyncMaterial req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            if (!req.ItemList.Any())
            {
                result.ErrMsg = "必须包含至少一个物料";
                return result;
            }

            var project = await _projectRepository.GetProjectByQuotationId(req.QuotationId);
            if (project == null)
            {
                result.ErrMsg = "没有找到对应报价单";
                return result;
            }

            // 人工物料费的基础数据
            var labourListFromDb = await _projectMaterialLabourRepository.GetListByProjectId(project.Id);

            // 主材分类基础数据
            var mainItemWithCategoryList = await _mainMaterialItemRepository.GetMaterialItemWithCategoryList();

            // 地采分类基础数据
            var localItemList = await _localMaterialItemRepository.GetList();

            // 人工工种分类
            var workTypeList = await _workTypeRepository.GetWorkTypeList();

            // 获取一个项目某个主材下的所有主材和地采
            var itemListFromDb = await _projectMaterialRepository.GetProjectMaterialItemListByProjectId(project.Id);
            var insertList = new List<T_ProjectMaterialItem>();
            var updateList = new List<T_ProjectMaterialItem>();
            var deleteList = new List<T_ProjectMaterialItem>();
            var errorList = new List<RespBatchSyncMaterial>();
            var labourList = new List<T_ProjectMaterialLabour>();
            foreach (var itemFromReq in req.ItemList)
            {
                // 是人工费，单独处理, 判断是否重复，如果重复，则不插入数据库
                if (itemFromReq.Type == EnumMaterialType.Labour
                    && !labourListFromDb.Exists(lab =>
                        lab.ProjectId == project.Id && lab.MaterialNo == itemFromReq.MaterialNo &&
                        lab.Space == itemFromReq.Space))
                {
                    var labour = new T_ProjectMaterialLabour();
                    // 是否符合数据库里的编码规则
                    var checkLabour = workTypeList.Exists(w => w.No == itemFromReq.MaterialNo);
                    if (checkLabour)
                    {
                        labour.MaterialNo = itemFromReq.MaterialNo;
                        labour.Space = itemFromReq.Space;
                        labour.MaterialName = itemFromReq.MaterialName;
                        labour.ProjectId = project.Id;
                        labour.MaterialPrice = itemFromReq.MaterialPrice;
                        labour.Quantity = itemFromReq.Quantity;
                        labour.DetailsId = itemFromReq.DetailsId;

                        labourList.Add(labour);
                    }
                    else
                    {
                        errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space,
                            "该人工物料费用在后台没有找到对应的编码规则"));
                    }
                }
                else
                {
                    // 查找匹配规则
                    var checkNoMatch = MaterialHelper.CheckMaterial(mainItemWithCategoryList, localItemList, itemFromReq, out var matchResult);

                    if (!checkNoMatch)
                    {
                        errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space, "该物料在后台没有找到对应的编码规则"));
                    }
                    else
                    {
                        // 物料编码和空间作为唯一匹配
                        var itemFromDb = itemListFromDb.FirstOrDefault(m => m.MaterialNo == itemFromReq.MaterialNo && m.Space == itemFromReq.Space);

                        if (itemFromReq.SyncType == EnumSyncType.Insert)
                        {
                            if (itemFromDb == null)
                            {
                                // 数据库中没有，则新增
                                var material = MaterialHelper.GeneMaterialItemWhenSync(project.Id, itemFromReq, matchResult);
                                insertList.Add(material);
                            }
                            else
                            {
                                errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space, "该物料已存在，不可新增"));
                            }

                        }
                        else if (itemFromReq.SyncType == EnumSyncType.Update)
                        {
                            if (itemFromDb == null)
                            {
                                errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space, "该物料不存在，不可修改"));
                            }
                            else if (itemFromDb.Status == EnumMaterialItemStatus.ToSubmitOrder2)
                            {
                                errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space, "该物料已经测量完成，不可修改"));
                            }
                            else
                            {
                                // 除了状态不可更改，其他都可变
                                var material = MaterialHelper.UpdateMaterialItemSync(itemFromDb, itemFromReq);
                                updateList.Add(material);
                            }
                        }
                        else if (itemFromReq.SyncType == EnumSyncType.Delete)
                        {
                            if (itemFromDb == null)
                            {
                                errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space, "该物料不存在，不可删除"));
                            }
                            else if (itemFromDb.Status == EnumMaterialItemStatus.ToSubmitOrder2)
                            {
                                errorList.Add(new RespBatchSyncMaterial(itemFromReq.MaterialNo, itemFromReq.Space, "该物料已经测量完成，不可删除"));
                            }
                            else
                            {
                                var material = new T_ProjectMaterialItem();
                                material.ProjectId = project.Id;
                                material.Space = itemFromReq.Space;
                                material.MaterialNo = itemFromReq.MaterialNo;

                                deleteList.Add(material);
                            }
                        }
                    }
                }
            }

            if (errorList.Any())
            {
                result.ErrMsg = "存在错误状态的物料";
                result.Data = errorList;
                return result;
            }

            await _projectMaterialRepository.SyncMaterialFromU9(
                _projectMaterialItemLogRepository,
                _projectMaterialLabourRepository,
                labourList,
                insertList,
                updateList,
                deleteList);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 【APP使用】主材管理，APP主材管理列表页面（无分页）
        /// </summary>
        [HttpGet("MaterialList")]
        public async Task<ResponseMessage> MaterialList([FromQuery]ReqGetProjectMaterial req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var resultList = new List<RespGetProjectMaterial>();
            // 获取一个项目某个主材下的所有主材明细，如瓷砖下的所有瓷砖
            var data = await _projectMaterialRepository.GetProjectMaterialItemListByProjectId(req.ProjectId, EnumMaterialType.Main);
            if (!data.Any())
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = resultList;
                return result;
            }
            else
            {
                // 先生成一级分类（一级分类、二级分类等）
                foreach (T_ProjectMaterialItem item in data)
                {
                    if (!resultList.Exists(r=>r.CategoryId == item.CategoryId))
                    {
                        var respGetProjectMaterial = new RespGetProjectMaterial();
                        respGetProjectMaterial.CategoryName = item.CategoryName;
                        respGetProjectMaterial.CategoryId = item.CategoryId;
                    }
                }

                var materialList = new List<Material>();
                // 再生成二级分类（瓷砖、龙头、吊顶）
                foreach (T_ProjectMaterialItem item in data)
                {
                    // 如果没有二级分类，创建二级分类
                    if (!materialList.Exists(r => r.SecondCategoryId == item.SecondCategoryId))
                    {
                        var material = new Material();
                        material.CategoryId = item.CategoryId;
                        material.CategoryName = item.CategoryName;
                        material.SecondCategoryId = item.SecondCategoryId;
                        material.SecondCategoryName = item.SecondCategoryName;
                        // 测量状态
                        var measuredCount = data.Count(d => d.MeasureStatus);
                        if (measuredCount == 0)
                        {
                            material.MeasureStatus = EnumStatusStage.NotStart;
                        }
                        else if(measuredCount == data.Count)
                        {
                            material.MeasureStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.MeasureStatus = EnumStatusStage.Doing;
                        }
                        // 订单状态
                        var orderCount = data.Count(d => d.OrderStatus);
                        if (orderCount == 0)
                        {
                            material.OrderStatus = EnumStatusStage.NotStart;
                        }
                        else if (orderCount == data.Count)
                        {
                            material.OrderStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.OrderStatus = EnumStatusStage.Doing;
                        }
                        // 订单确认状态
                        var orderConfirmCount = data.Count(d => d.ConfirmOrderStatus);
                        if (orderConfirmCount == 0)
                        {
                            material.ConfirmOrderStatus = EnumStatusStage.NotStart;
                        }
                        else if (orderConfirmCount == data.Count)
                        {
                            material.ConfirmOrderStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.ConfirmOrderStatus = EnumStatusStage.Doing;
                        }
                        // 出库状态
                        var deliveryCount = data.Count(d => d.DeliveryStatus);
                        if (deliveryCount == 0)
                        {
                            material.DeliveryStatus = EnumStatusStage.NotStart;
                        }
                        else if (deliveryCount == data.Count)
                        {
                            material.DeliveryStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.DeliveryStatus = EnumStatusStage.Doing;
                        }
                        // 入库状态
                        var inStorageCount = data.Count(d => d.InStorageStatus);
                        if (inStorageCount == 0)
                        {
                            material.InStorageStatus = EnumStatusStage.NotStart;
                        }
                        else if (inStorageCount == data.Count)
                        {
                            material.InStorageStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.InStorageStatus = EnumStatusStage.Doing;
                        }
                        // 出库申请状态
                        var outStorageCount = data.Count(d => d.OutStorageApplyStatus);
                        if (outStorageCount == 0)
                        {
                            material.OutStorageApplyStatus = EnumStatusStage.NotStart;
                        }
                        else if (outStorageCount == data.Count)
                        {
                            material.OutStorageApplyStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.OutStorageApplyStatus = EnumStatusStage.Doing;
                        }
                        // 确认收货状态
                        var confirmReceiveCount = data.Count(d => d.ConfirmReceiveStatus);
                        if (confirmReceiveCount == 0)
                        {
                            material.ConfirmReceiveStatus = EnumStatusStage.NotStart;
                        }
                        else if (confirmReceiveCount == data.Count)
                        {
                            material.ConfirmReceiveStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.ConfirmReceiveStatus = EnumStatusStage.Doing;
                        }
                        // 安装状态
                        var installCount = data.Count(d => d.MeasureStatus);
                        if (installCount == 0)
                        {
                            material.InstallStatus = EnumStatusStage.NotStart;
                        }
                        else if (installCount == data.Count)
                        {
                            material.InstallStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.InstallStatus = EnumStatusStage.Doing;
                        }
                        // 结算状态
                        var settlementCount = data.Count(d => d.SettlementStatus);
                        if (settlementCount == 0)
                        {
                            material.SettlementStatus = EnumStatusStage.NotStart;
                        }
                        else if (settlementCount == data.Count)
                        {
                            material.SettlementStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.SettlementStatus = EnumStatusStage.Doing;
                        }
                       
                        materialList.Add(material);
                    }
                }

                // 归类所有的二级分类

                foreach (Material material in materialList)
                {
                    // 找到对应的一级分类
                    var materialCategory = resultList.FirstOrDefault(r => r.CategoryId == material.CategoryId);
                    if (materialCategory == null)
                    {
                        materialCategory = new RespGetProjectMaterial();
                        materialCategory.CategoryId = material.CategoryId;
                        materialCategory.CategoryName = material.CategoryName;

                        materialCategory.MaterialList.Add(material);

                        resultList.Add(materialCategory);
                    }
                    else
                    {
                        materialCategory.MaterialList.Add(material);
                    }
                }
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = resultList;
                return result;
            }
        }

        /// <summary>
        /// 【APP使用】地采管理，APP地采管理列表页面（无分页）
        /// </summary>
        [HttpGet("LocalMaterialList")]
        public async Task<ResponseMessage> LocalMaterialList([FromQuery]ReqGetProjectLocalMaterial req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取一个项目某个主材下的所有地采明细，如不锈钢下的所有不锈钢
            var data = await _projectMaterialRepository.GetProjectMaterialItemListByProjectId(req.ProjectId, EnumMaterialType.Local);
            var materialList = new List<LocalMaterial>();
            if (!data.Any())
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = materialList;

                return result;
            }
            else
            {
                // 生成二级分类
                foreach (T_ProjectMaterialItem item in data)
                {
                    // 如果没有二级分类，创建二级分类
                    if (!materialList.Exists(r => r.SecondCategoryId == item.SecondCategoryId))
                    {
                        var material = new LocalMaterial();
                        material.SecondCategoryId = item.SecondCategoryId;
                        material.SecondCategoryName = item.SecondCategoryName;

                        // 测量状态
                        var measureCount = data.Count(d => d.MeasureStatus);
                        if (measureCount == 0)
                        {
                            material.MeasureStatus = EnumStatusStage.NotStart;
                        }
                        else if (measureCount == data.Count)
                        {
                            material.MeasureStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.MeasureStatus = EnumStatusStage.Doing;
                        }
                        // 订单状态
                        var orderCount = data.Count(d => d.OrderStatus);
                        if (orderCount == 0)
                        {
                            material.OrderStatus = EnumStatusStage.NotStart;
                        }
                        else if (orderCount == data.Count)
                        {
                            material.OrderStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.OrderStatus = EnumStatusStage.Doing;
                        }
                        // 订单确认状态
                        var orderConfirmCount = data.Count(d => d.ConfirmOrderStatus);
                        if (orderConfirmCount == 0)
                        {
                            material.ConfirmOrderStatus = EnumStatusStage.NotStart;
                        }
                        else if (orderConfirmCount == data.Count)
                        {
                            material.ConfirmOrderStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.ConfirmOrderStatus = EnumStatusStage.Doing;
                        }
                        // 确认收货状态
                        var confirmReceiveCount = data.Count(d => d.ConfirmReceiveStatus);
                        if (confirmReceiveCount == 0)
                        {
                            material.ConfirmReceiveStatus = EnumStatusStage.NotStart;
                        }
                        else if (confirmReceiveCount == data.Count)
                        {
                            material.ConfirmReceiveStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.ConfirmReceiveStatus = EnumStatusStage.Doing;
                        }
                        // 安装状态
                        var installCount = data.Count(d => d.MeasureStatus);
                        if (installCount == 0)
                        {
                            material.InstallStatus = EnumStatusStage.NotStart;
                        }
                        else if (installCount == data.Count)
                        {
                            material.InstallStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.InstallStatus = EnumStatusStage.Doing;
                        }
                        // 结算状态
                        var settlementCount = data.Count(d => d.SettlementStatus);
                        if (settlementCount == 0)
                        {
                            material.SettlementStatus = EnumStatusStage.NotStart;
                        }
                        else if (settlementCount == data.Count)
                        {
                            material.SettlementStatus = EnumStatusStage.Finished;
                        }
                        else
                        {
                            material.SettlementStatus = EnumStatusStage.Doing;
                        }

                        materialList.Add(material);
                    }
                }

                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = materialList;
                return result;
            }
        }

        /// <summary>
        /// 【APP使用】获取某个分类下的主材明细列表
        /// </summary>
        [HttpGet("MaterialItemList")]
        public async Task<ResponseMessage> MaterialItemList([FromQuery]ReqGetProjectMaterialItemList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取一个项目某个主材下的所有主材明细，如瓷砖下的所有瓷砖
            var data = await _projectMaterialRepository.GetProjectMaterialItemListByProjectIdAndSecondCategoryId(req.ProjectId, req.SecondCategoryId);

            var resultData = new ArrayList();
            var list_0 = new MaterialItemList(0, "全部");

            var list_1 = new MaterialItemList(1, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeMeasure1));
            var list_2 = new MaterialItemList(2, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToSubmitOrder2));
            var list_3 = new MaterialItemList(3, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToConfirmOrder3));
            var list_4 = new MaterialItemList(4, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeDelivery4));

            var list_5 = new MaterialItemList(5, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeInStorage5));
            var list_6 = new MaterialItemList(6, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeOutStorageApply6));
            var list_7 = new MaterialItemList(7, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeConfirmReceive7));
            //待安装页面单独处理
            var list_8 = new MaterialItemList(8, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeInstall8));
            var list_9 = new MaterialItemList(9, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeSettlement9));
            foreach (var item in data)
            {
                var itemForResult = new RespGetProjectMaterialItem();
                itemForResult.Name = item.MaterialName;
                itemForResult.No = item.MaterialNo;
                itemForResult.Id = item.Id;
                itemForResult.Space = item.Space;
                itemForResult.Quantity = item.Quantity;
                itemForResult.Unit = item.Unit;
                // 首先加到全部里边
                list_0.ItemList.Add(itemForResult);
                // 然后加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    case EnumMaterialItemStatus.ToBeMeasure1:
                        list_1.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToSubmitOrder2:
                        list_2.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToConfirmOrder3:
                        list_3.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeDelivery4:
                        list_4.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeInStorage5:
                        list_5.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeOutStorageApply6:
                        list_6.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeConfirmReceive7:
                        list_7.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeInstall8:
                        // 数据库取出具体的安装详情
                        var itemForResult_8 = new RespGetProjectMaterialItem_Install();
                        var item_8InDb = await _projectInstallTaskRepository.SingleAsync(item.Id);
                        if (item_8InDb!=null)
                        {
                            itemForResult_8.Note = item_8InDb.Remark;
                            itemForResult_8.Id = item_8InDb.Id;
                            if (item_8InDb.InstallDate != null)
                                itemForResult_8.InstallTime = item_8InDb.InstallDate.ToLongTimeString();
                            itemForResult_8.InstallWorker = item_8InDb.WorkerName;
                            itemForResult_8.Phone = item_8InDb.WorkerPhone;
                            itemForResult_8.Id = item_8InDb.Id;
                            list_8.ItemList.Add(itemForResult_8);
                        }

                        break;
                    case EnumMaterialItemStatus.ToBeSettlement9:
                        list_9.ItemList.Add(itemForResult);
                        break;
                    default:
                        break;
                }
            }

            // 统一添加到结果上
            resultData.Add(list_0);
            resultData.Add(list_1);
            resultData.Add(list_2);
            resultData.Add(list_3);
            resultData.Add(list_4);
            resultData.Add(list_6);
            resultData.Add(list_7);
            resultData.Add(list_5);
            resultData.Add(list_8);
            resultData.Add(list_9);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 【APP使用】获取某个分类下的地采明细列表
        /// </summary>
        [HttpGet("LocalMaterialItemList")]
        public async Task<ResponseMessage> LocalMaterialItemList([FromQuery]ReqGetProjectLocalMaterialItemList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取一个项目某个地采下的所有主材明细，如瓷砖下的所有瓷砖
            var data = await _projectMaterialRepository.GetProjectMaterialItemListByProjectIdAndSecondCategoryId(req.ProjectId, req.SecondCategoryId, EnumMaterialType.Local);

            var resultData = new ArrayList();
            var list_0 = new MaterialItemList(0, "全部");

            var list_1 = new MaterialItemList(1, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeMeasure1));
            var list_2 = new MaterialItemList(2, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToSubmitOrder2));
            var list_3 = new MaterialItemList(3, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToConfirmOrder3));
            var list_7 = new MaterialItemList(4, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeConfirmReceive7));
            var list_8 = new MaterialItemList(8, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeInstall8));
            //带安装页面单独处理
            var list_9 = new MaterialItemList(9, EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), EnumMaterialItemStatus.ToBeSettlement9));
            foreach (var item in data)
            {
                var itemForResult = new RespGetProjectMaterialItem();
                itemForResult.Name = item.MaterialName;
                itemForResult.Id = item.Id;
                itemForResult.Space = item.Space;
                itemForResult.No = item.MaterialNo;
                itemForResult.Quantity = item.Quantity;
                itemForResult.Unit = item.Unit;
                // 首先加到全部里边
                list_0.ItemList.Add(itemForResult);
                // 然后加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    case EnumMaterialItemStatus.ToBeMeasure1:
                        list_1.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToSubmitOrder2:
                        list_2.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToConfirmOrder3:
                        list_3.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeConfirmReceive7:
                        list_7.ItemList.Add(itemForResult);
                        break;
                    case EnumMaterialItemStatus.ToBeInstall8:
                        // 数据库取出具体的安装详情
                        var itemForResult_8 = new RespGetProjectMaterialItem_Install();
                        var item_8InDb = await _projectInstallTaskRepository.SingleAsync(item.Id);
                        if (item_8InDb != null)
                        {
                            itemForResult_8.Note = item_8InDb.Remark;
                            itemForResult_8.Id = item_8InDb.Id;
                            if (item_8InDb.InstallDate != null)
                                itemForResult_8.InstallTime = item_8InDb.InstallDate.ToLongTimeString();
                            itemForResult_8.InstallWorker = item_8InDb.WorkerName;
                            itemForResult_8.Phone = item_8InDb.WorkerPhone;
                            itemForResult_8.Id = item_8InDb.Id;
                            list_9.ItemList.Add(itemForResult_8);
                        }

                        break;
                    case EnumMaterialItemStatus.ToBeSettlement9:
                        list_9.ItemList.Add(itemForResult);
                        break;
                    default:
                        break;
                }
            }

            // 统一添加到结果上
            resultData.Add(list_0);
            resultData.Add(list_1);
            resultData.Add(list_2);
            resultData.Add(list_3);
            resultData.Add(list_7);
            resultData.Add(list_8);
            resultData.Add(list_9);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 【APP使用】根据物料id获取物料详情
        /// </summary>
        [HttpGet("Detail")]
        public async Task<ResponseMessage> Detail([FromQuery]ReqGetDetail req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var materialItem = await _projectMaterialRepository.SingleAsync(req.ProjectMaterialItemId);
            var detail = new RespGetDetail(materialItem);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = detail;
            return result;
        }

        /// <summary>
        /// 【APP使用】获取某个分类下的主材的测量任务列表
        /// </summary>
        [HttpGet("MeasureTaskList")]
        public async Task<ResponseMessage> MeasureTaskList([FromQuery]ReqGetProjectMeasureTaskList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取一个项目某个主材下的所有测量任务列表
            var data = await _projectMeasureTaskRepository.GetMeasureTaskList(req.ProjectId, req.SecondCategoryId);

            var resultData = new ArrayList();
            //任务状态：1 待开工 2 已开工 3 已完成

            //var list_1 = new MaterialItemList(1, "待开工");
            //var list_2 = new MaterialItemList(2, "已开工");
            var list_3 = new MaterialItemList(3, "已完成");

            foreach (var item in data)
            {
                // 加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    //case EnumMeasureTaskStatus.NotStart:
                    //    list_1.ItemList.Add(item);
                    //    break;
                    //case EnumMeasureTaskStatus.Working:
                    //    list_2.ItemList.Add(item);
                    //    break;
                    case EnumMeasureTaskStatus.Finished:
                        list_3.ItemList.Add(item);
                        break;

                    default:
                        break;
                }
            }

            // 统一添加到结果上
            //resultData.Add(list_1);
            //resultData.Add(list_2);
            resultData.Add(list_3);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 【APP使用】提交测量任务并回写U9
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SubmitMeasureTask")]
        [AllowAnonymous]
        public async Task<ResponseMessage> SubmitMeasureTask(ReqSubmitMeasureTask request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            if (!request.MeasureTaskList.Any())
            {
                result.ErrMsg = "必须至少包含一个物料";
                return result;
            }

            var project = await _projectRepository.SingleAsync(request.ProjectId);
            var taskList = new List<T_ProjectMeasureTask>();
            var taskItemList = new List<SubMeasureTaskItem>();
            foreach (var measureTaskFromReq in request.MeasureTaskList)
            {
                var task = new T_ProjectMeasureTask();
                task.Status = EnumMeasureTaskStatus.Finished;
                task.ProjectId = request.ProjectId;
                task.ProjectMaterialItemId = measureTaskFromReq.ProjectMaterialItemId;
                // 日期是项目的计划截止日期
                if (project.PlanEndDate != null) task.RequireDateTime = project.PlanEndDate.Value;
                task.UserId = request.UserId;
                task.TaskName = "测量任务";
                // 加物料Id，防止重名
                task.TaskNo = "CLRW" + DateTime.Now.ToString("yyyMMddHHmmss") + task.ProjectMaterialItemId;
                task.Type = request.Type;

                taskList.Add(task);

                // 把所有的测量任务都存储起来
                foreach (MeasureTaskItem item in measureTaskFromReq.TaskItemList)
                {
                    var taskItem = new SubMeasureTaskItem();
                    taskItem.Amount = item.Amount;
                    taskItem.Size = item.Size;
                    taskItem.Note = item.Note;
                    taskItem.ProjectMaterialItemId = task.ProjectMaterialItemId; 
                    taskItemList.Add(taskItem);
                }
            }

            var submitResult = await _projectMeasureTaskRepository.SubmitTask(taskList,taskItemList, _projectMaterialItemLogRepository, _projectMeasureTaskItemRepository, _projectMaterialRepository, request.TimeSign, request.Key);

            if (submitResult)
            {
                result.ErrCode = MessageResultCode.Success;
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.Data = "";
                return result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 【APP使用】测量任务详情
        /// </summary>
        [HttpGet("MeasureTaskDetail")]
        public async Task<ResponseMessage> MeasureTaskDetail([FromQuery]ReqGetMeasureTaskDetail req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var materialItem = await _projectMaterialRepository.SingleAsync(req.ProjectMaterialItemId);
            var task = await _projectMeasureTaskRepository.SingleAsync(req.TaskId);
            var taskItemList = await _projectMeasureTaskItemRepository.GetTaskItemListByTaskId(req.TaskId);
            var user = await GetUser(_projectRepository, req.ProjectId, task.UserId); ;

            var taskDetail = new RespGetMeasureTaskDetail(materialItem, task, user, taskItemList);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = taskDetail;
            return result;
        }

        /// <summary>
        /// 【APP使用】获取某个分类下的主材的订单任务列表
        /// </summary>
        [HttpGet("OrderTaskList")]
        public async Task<ResponseMessage> OrderTaskList([FromQuery]ReqGetProjectOrderTaskList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取一个项目某个主材下的所有测量任务列表
            var data = await _projectOrderTaskRepository.GetOrderTaskList(req.ProjectId, req.SecondCategoryId);
            
            var resultData = new ArrayList();

            var list_1 = new RespGetProjectOrderTaskList(1, "申请中");
            var list_2 = new RespGetProjectOrderTaskList(2, "已取消");
            var list_3 = new RespGetProjectOrderTaskList(3, "已完成");
            // 统一添加到结果上
            resultData.Add(list_1);
            resultData.Add(list_2);
            resultData.Add(list_3);

            if (!data.Any())
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = resultData;
                return result;
            }

            var projectMaterial = await _projectMaterialRepository.SingleAsync(data.First().ProjectMaterialItemId);

            foreach (var item in data)
            {
                var orderTask = new OrderItem();
                orderTask.Id = item.Id;
                orderTask.Status = item.Status;
                orderTask.MaterialTypeName = projectMaterial.SecondCategoryName;
                orderTask.StatusName = EnumHelper.GetDescription(typeof(EnumOrderTaskStatus), orderTask.Status);
                orderTask.CreateTime = item.CreateTime.ToString("yyyy-MM-dd");
                orderTask.MaterialName = projectMaterial.MaterialName;
                orderTask.TaskNo = item.TaskNo;
                if (item.PlanUseTime != null)
                    orderTask.PlanUseTime = item.PlanUseTime.ToString("yyyy-MM-dd");

                // 加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    case EnumOrderTaskStatus.Submit:
                        list_1.ItemList.Add(orderTask);
                        break;
                    case EnumOrderTaskStatus.Canceled:
                        list_2.ItemList.Add(orderTask);
                        break;
                    case EnumOrderTaskStatus.Finished:
                        list_3.ItemList.Add(orderTask);
                        break;

                    default:
                        break;
                }
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 【APP使用】提交订单任务，回写U9
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SubmitOrderTask")]
        [AllowAnonymous]
        public async Task<ResponseMessage> SubmitOrderTask(ReqSubmitOrderTask request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (!request.OrderTaskList.Any())
            {
                result.ErrMsg = "必须至少包含一个物料";
                return result;
            }

            var project = await _projectRepository.SingleAsync(request.ProjectId);

            var taskList = new List<T_ProjectOrderTask>();
            foreach (var orderTaskFromReq in request.OrderTaskList)
            {
                var task = new T_ProjectOrderTask();
                task.Status = EnumOrderTaskStatus.Submit;
                //task.IsUrgency = orderTaskFromReq.IsUrgency;
                //task.PlanUseTime = orderTaskFromReq.PlanUseTime;
                task.ProjectId = request.ProjectId;
                task.ProjectMaterialItemId = orderTaskFromReq.ProjectMaterialItemId;
                task.UserId = request.UserId;
                task.Type = request.Type;
                task.PlanUseTime = orderTaskFromReq.PlanDateTime;
                task.Remark = orderTaskFromReq.Remark;
                task.Quantity = orderTaskFromReq.Quantity;
                task.TaskName = "订单任务";
                // 加物料Id，防止重名
                task.TaskNo = "DDRW" + DateTime.Now.ToString("yyyMMddHHmmss") + task.ProjectMaterialItemId;

                taskList.Add(task);
            }

            var submitResult = await _projectOrderTaskRepository.SubmitOrderTask(
                project.ProjectNo,
                request.Remark,
                request.OrderType,
                request.Supplier,
                taskList,
                _projectMaterialItemLogRepository,
                _projectMaterialRepository,
                request.TimeSign,
                request.Key,
                request.UserId);

            if (submitResult)
            {
                result.ErrCode = MessageResultCode.Success;
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.Data = "";
                return result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 【APP使用】订单任务详情
        /// </summary>
        [HttpGet("OrderTaskDetail")]
        public async Task<ResponseMessage> OrderTaskDetail([FromQuery]ReqGetOrderTaskDetail req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var materialItem = await _projectMaterialRepository.SingleAsync(req.ProjectMaterialItemId);
            var task = await _projectOrderTaskRepository.SingleAsync(req.TaskId);
            var user = await GetUser(_projectRepository, req.ProjectId, task.UserId); //GetUser(task.UserId);

            var taskDetail = new RespGetOrderTaskDetail(materialItem, task, user);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = taskDetail;
            return result;
        }

        /// <summary>
        /// 【APP使用】物料状态日志
        /// </summary>
        [HttpGet("Logs")]
        public async Task<ResponseMessage> Logs([FromQuery]ReqGetLog req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var logListFromDb = await _projectMaterialItemLogRepository.GetLogListByItemId(req.ProjectMaterialItemId);

            var logList = new List<ReqLog>();
            for (int i = 0; i < logListFromDb.Count-1; i++)
            {
                var itemLog = logListFromDb[i];
                var log = new ReqLog();
                log.CreateTime = itemLog.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (i == logListFromDb.Count - 1)
                {
                    // 最后一条显示“待。。。”
                    log.CurrentStatus = itemLog.StatusName;
                }
                else
                {
                    // 其他显示“已。。。”
                    log.CurrentStatus = itemLog.Remark;
                }

                logList.Add(log);
            }

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = logList;
            return result;
        }

        /// <summary>
        /// todo 【APP使用】 该功能不做暂时 取消订单任务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CancelOrderTask")]
        [AllowAnonymous]
        public async Task<ResponseMessage> CancelOrderTask(ReqCancelOrderTask request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var orderTask = await _projectOrderTaskRepository.SingleAsync(request.TaskId);
            orderTask.Status = EnumOrderTaskStatus.Canceled;//已取消
            orderTask.CancelReason = request.CancelReason;
            orderTask.EditTime = DateTime.Now;

            await _projectOrderTaskRepository.UpdateAsync(orderTask);

            result.ErrCode = MessageResultCode.Success;
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 【APP使用】获取某个分类下的主材的安装任务列表
        /// </summary>
        [HttpGet("InstallTaskList")]
        public async Task<ResponseMessage> InstallTaskList([FromQuery]ReqGetProjectInstallTaskList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            // 获取一个项目某个主材下的所有测量任务列表
            var data = await _projectInstallTaskRepository.GetInstallTaskList(req.ProjectId, req.SecondCategoryId);

            var resultData = new ArrayList();
            //状态：1 待开工 2 已开工 3 已完成

            //var list_1 = new InstallTaskList(1, "待开工");
            //var list_2 = new InstallTaskList(2, "已开工");
            var list_3 = new InstallTaskList(3, "已完成");

            foreach (var item in data)
            {
                var installTask = new InstallItem();
                installTask.Status = item.Status;
                installTask.Note = item.Remark;
                installTask.Id = item.Id;
                if (item.InstallDate != null)
                    installTask.InstallTime = item.InstallDate.ToLongTimeString();
                installTask.InstallWorker = item.WorkerName;
                installTask.Phone = item.WorkerPhone;
                installTask.Id = item.Id;

                // 加到具体的某一个状态的list里里
                switch (item.Status)
                {
                    //case EnumInstallTaskStatus.NotStart:
                    //    list_1.ItemList.Add(installTask);
                    //    break;
                    //case EnumInstallTaskStatus.Working:
                    //    list_2.ItemList.Add(installTask);
                    //    break;
                    case EnumInstallTaskStatus.Finished:
                        list_3.ItemList.Add(installTask);
                        break;

                    default:
                        break;
                }
            }

            // 统一添加到结果上
            //resultData.Add(list_1);
            //resultData.Add(list_2);
            resultData.Add(list_3);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = resultData;
            return result;
        }

        /// <summary>
        /// 【APP使用】提交安装任务，回写U9
        /// 回写U9
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SubmitInstallTask")]
        [AllowAnonymous]
        public async Task<ResponseMessage> SubmitInstallTask(ReqSubmitInstallTask request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            if (!request.InstallTaskList.Any())
            {
                result.ErrMsg = "必须至少包含一个物料";
                return result;
            }
            var taskList = new List<T_ProjectInstallTask>();
            foreach (var installTask in request.InstallTaskList)
            {
                var task = new T_ProjectInstallTask();
                task.Status = EnumInstallTaskStatus.Finished;
                task.ProjectId = request.ProjectId;
                task.ProjectMaterialItemId = installTask.ProjectMaterialItemId;
                task.Remark = installTask.Remark;
                task.InstallDate = installTask.InstallDate;
                task.UserId = request.UserId;
                task.Type = request.Type;
                task.TaskName = "安装任务";
                task.TaskNo = "AZRW" + DateTime.Now.ToString("yyyMMddHHmmss") + task.ProjectMaterialItemId;

                taskList.Add(task);
            }

            var submitResult = await _projectInstallTaskRepository.SubmitTask(taskList, _projectMaterialItemLogRepository, _projectMaterialRepository, request.TimeSign, request.Key);

            if (submitResult)
            {
                result.ErrCode = MessageResultCode.Success;
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.Data = "";
                return result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 【APP使用】安装任务详情
        /// </summary>
        [HttpGet("InstallTaskDetail")]
        public async Task<ResponseMessage> InstallTaskDetail([FromQuery]ReqGetInstallTaskDetail req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var materialItem = await _projectMaterialRepository.SingleAsync(req.ProjectMaterialItemId);
            var task = await _projectInstallTaskRepository.SingleAsync(req.TaskId);
            var user = await GetUser(_projectRepository, req.ProjectId, task.UserId);// GetUser(task.UserId);

            var taskDetail = new RespGetInstallTaskDetail(materialItem, task, user);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = taskDetail;
            return result;
        }

        /// <summary>
        /// 【APP使用】提交领料（每次只一个物料）；物料在一个项目里的唯一索引是编码+空间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("PickMaterial")]
        [AllowAnonymous]
        public async Task<ResponseMessage> PickMaterial(ReqSubmitPickMaterial request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };
            var material = await _projectMaterialRepository.GetProjectMaterialItemByNoAndSpace(request.ProjectId, request.MaterialNo, request.Space);
            if (material == null)
            {
                result.ErrMsg = "发包数据里没有对应编码和空间的物料";
                return result;
            }
            else
            {
                var pickItem = new T_ProjectPickMaterial();
                pickItem.Status = EnumPickMaterialStatus.Apply;
                pickItem.Amount = request.Amount;
                pickItem.MaterialName = material.MaterialName;
                pickItem.Quantity = pickItem.Quantity;
                pickItem.Space = request.Space;
                pickItem.Remark = request.Remark;
                pickItem.MaterialNo = request.MaterialNo;
                pickItem.PickNo = "LLGL" + DateTime.Now.ToString("yyyyMMddHHmmss") + material.Id;
                pickItem.ProjectId = request.ProjectId;
                pickItem.ProjectMaterialId = material.Id;

                MaterialMachine.OutStorageApply(ref material);

                await _projectPickMaterialRepository.Pick(_projectMaterialRepository, _projectMaterialItemLogRepository, pickItem, material, request.TimeSign, request.Key);
            }
            result.ErrCode = MessageResultCode.Success;
            result.ErrMsg = CommonMessage.OperateSuccess;
            result.Data = "";
            return result;
        }

        /// <summary>
        /// 【APP使用】领料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("PickMaterialList")]
        [AllowAnonymous]
        public async Task<ResponseMessage> PickMaterialList([FromQuery]ReqGetPickmaterialList req)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            // 获取一个项目某个地采下的所有主材明细，如瓷砖下的所有瓷砖
            var data = await _projectPickMaterialRepository.GetProjectPickMaterialListByProjectId(req.ProjectId);

            var resultData = new ArrayList();

            var list_1 = new PickMaterialItemList(1, EnumHelper.GetDescription(typeof(EnumPickMaterialStatus), EnumPickMaterialStatus.Apply));
            var list_2 = new PickMaterialItemList(2, EnumHelper.GetDescription(typeof(EnumPickMaterialStatus), EnumPickMaterialStatus.Finished));
            if (data.Any())
            {
                foreach (var item in data)
                {
                    var itemForResult = new RespGetPickMaterialList(item);

                    // 首先加到全部里边
                    switch (item.Status)
                    {
                        case EnumPickMaterialStatus.Apply:
                            list_1.ItemList.Add(itemForResult);
                            break;
                        case EnumPickMaterialStatus.Finished:
                            list_2.ItemList.Add(itemForResult);
                            break;

                        default:
                            break;
                    }
                }

                // 统一添加到结果上
                resultData.Add(list_1);
                resultData.Add(list_2);

                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = resultData;
                return result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 【U9使用】U9内部(订单确认，总部发货，分公司入库)，通知APP物料的状态，用于和U9交互
        /// </summary>
        [HttpPost("BatchUpdateOrderFromU9")]
        public async Task<ResponseMessage> BatchUpdateOrderFromU9(ReqOrderConfirmedFromU9 request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            if (!request.OrderTaskIdList.Any())
            {
                result.ErrMsg = "必须包含至少一个物料";
                return result;
            }

            var project = await _projectRepository.GetProjectByQuotationId(request.QuotationId);
            if (project == null)
            {
                result.ErrMsg = "没有找到对应报价单";
                return result;
            }

            // 获取一个项目所有主材和地采
            var orderTaskListFromDb = await _projectOrderTaskRepository.GetOrderTaskListByProjectId(project.Id);
            var updateList = new List<T_ProjectOrderTask>();
            var materialList = new List<T_ProjectMaterialItem>();
            var errorList = new List<string>();

            foreach (var taskIdFromReq in request.OrderTaskIdList)
            {
                // 物料编码和空间作为唯一匹配
                var orderTaskFromDb = orderTaskListFromDb.FirstOrDefault(m => m.Id == taskIdFromReq);
                if (orderTaskFromDb == null)
                {
                    // 数据库中没有，返回错误
                    errorList.Add(@$"该TaskId{taskIdFromReq}编码在后台没有找到对应的物料");
                }
                else if (request.Type == EnumMaterialType.Local && request.Status != EnumOrderStatusFromReq.OrderConfirmed)
                {
                    errorList.Add(@$"该TaskId{taskIdFromReq}属于地采，不可以有出库和入库操作");
                }
                else
                {
                    var material = await _projectMaterialRepository.SingleAsync(orderTaskFromDb.ProjectMaterialItemId);
                    orderTaskFromDb.Status = EnumOrderTaskStatus.Finished;
                    orderTaskFromDb.EditTime = DateTime.Now;

                    // 订单确认
                    if (request.Status == EnumOrderStatusFromReq.OrderConfirmed)
                    {
                        MaterialMachine.ConfirmOrder(ref material);
                        if (request.Type == EnumMaterialType.Main)
                        {
                            orderTaskFromDb.OrderStatus = EnumMaterialItemStatus.ToBeDelivery4;
                        }
                        else
                        {
                            // 地采没有出库，入库等过程
                            orderTaskFromDb.OrderStatus = EnumMaterialItemStatus.ToBeConfirmReceive7;
                        }
                    }
                    // 总部发货
                    else if (request.Status == EnumOrderStatusFromReq.Delivery)
                    {
                        orderTaskFromDb.OrderStatus = EnumMaterialItemStatus.ToBeInStorage5;
                        MaterialMachine.Delivery(ref material);
                    }
                    //分公司入库
                    else if (request.Status == EnumOrderStatusFromReq.InStorage)
                    {
                        orderTaskFromDb.OrderStatus = EnumMaterialItemStatus.ToBeOutStorageApply6;
                        MaterialMachine.InStorage(ref material);
                    }

                    updateList.Add(orderTaskFromDb);
                    materialList.Add(material);
                }
            }

            if (errorList.Any())
            {
                result.ErrMsg = "存在错误状态的物料";
                result.Data = errorList;
                return result;
            }

            var updateResult = await _projectOrderTaskRepository.BatchUpdateOrderFromU9(updateList, _projectMaterialRepository, _projectMaterialItemLogRepository, materialList);
            if (updateResult)
            {
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.ErrCode = MessageResultCode.Success;
                result.Data = "";
                return result;
            }

            return result;
        }

        /// <summary>
        /// 【APP使用】签收物料，并回写U9
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ConfirmReceive")]
        [AllowAnonymous]
        public async Task<ResponseMessage> ConfirmReceive(ReqConfirmReceive request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            if (!request.PickIdList.Any())
            {
                result.ErrMsg = "必须包含多个领料任务";

                return result;
            }

            var materialList = new List<T_ProjectMaterialItem>();
            var pickTaskList = new List<T_ProjectPickMaterial>();
            foreach (int pickId in request.PickIdList)
            {
                var pickTask = await _projectPickMaterialRepository.SingleAsync(pickId);
                pickTask.Status = EnumPickMaterialStatus.Finished;
                pickTask.EditTime = DateTime.Now;

                // 处理为领料状态
                var material = await _projectMaterialRepository.SingleAsync(pickTask.ProjectMaterialId);
                MaterialMachine.ConfirmReceive(ref material);

                materialList.Add(material);
                pickTaskList.Add(pickTask);
            }

            var submitResult = await _projectPickMaterialRepository.ConfirmReceive(
                _projectMaterialRepository,
                _projectMaterialItemLogRepository,
                materialList,
                pickTaskList,
                request.TimeSign,
                request.Key
            );

            if (submitResult)
            {
                result.ErrCode = MessageResultCode.Success;
                result.ErrMsg = CommonMessage.OperateSuccess;
                result.Data = "";
                return result;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// todo暂定，应该是项目结束以后统一批量改为安装费结算状态。安装费结算
        /// </summary>
        [HttpPost("ConfirmSettlementFromU9")]
        public async Task<ResponseMessage> ConfirmSettlementFromU9(ReqConfirmSettlementFromU9 request)
        {
            var result = new ResponseMessage
            {
                ErrMsg = CommonMessage.OperateFailed,
                ErrCode = MessageResultCode.Error,
                Data = 0
            };

            var project = await _projectRepository.GetProjectByQuotationId(request.QuotationId);

            if (project == null)
            {
                result.ErrMsg = "没有找到对应报价单";
                return result;
            }

            await _projectMaterialRepository.ConfirmSettlementFromU9(project.Id);

            result.ErrMsg = CommonMessage.OperateSuccess;
            result.ErrCode = MessageResultCode.Success;
            result.Data = "";
            return result;
        }
    }
}
