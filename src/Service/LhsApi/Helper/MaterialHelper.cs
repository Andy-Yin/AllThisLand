using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request.Material;

namespace LhsApi.Helper
{
    public static class MaterialHelper
    {
        /// <summary>
        /// 判断一个物料是否需要测量或者下单，以及物料分类等信息
        /// </summary>
        public static bool CheckMaterial(
            List<MainMaterialItemWithCategory> mainMaterialCategories,
            List<T_LocalMaterialItem> localMaterialItems,
            BatchSyncMaterialItem itemFromReq,
            out MaterialDate data)
        {
            data = new MaterialDate();

            // 取出前缀
            var expNo = itemFromReq.MaterialNo.Substring(0, 3);
            if (itemFromReq.Type == EnumMaterialType.Local)
            {
                var localMaterial = localMaterialItems.FirstOrDefault(l => l.Code == expNo);
                if (localMaterial == null)
                {
                    data = null;
                    return false;
                }
                else
                {
                    // 地采没有一级分类
                    //data.NeedMeasure = localMaterial.NeedMeasure;
                    //data.NeedOrder = localMaterial.NeedOrder;
                    data.SecodCategoryId = localMaterial.Id;
                    data.SecodCategoryName = localMaterial.Name;
                }
            }
            else
            {
                var mainMaterial = mainMaterialCategories.FirstOrDefault(l => l.Code == expNo);
                if (mainMaterial == null)
                {
                    data = null;
                    return false;
                }
                else
                {
                    data.CategoryId = mainMaterial.CategoryId;
                    data.CategoryName = mainMaterial.CategoryName;
                    //data.NeedMeasure = mainMaterial.NeedMeasure;
                    //data.NeedOrder = mainMaterial.NeedOrder;
                    data.SecodCategoryId = mainMaterial.Id;
                    data.SecodCategoryName = mainMaterial.Name;
                }
            }

            return true;
        }

        public static T_ProjectMaterialItem GeneMaterialItemWhenSync(int projectId, BatchSyncMaterialItem batchItem, MaterialDate data)
        {
            var item = new T_ProjectMaterialItem();
            item.MaterialNo = batchItem.MaterialNo;
            item.ProjectId = projectId;
            item.Type = batchItem.Type;
            item.CategoryId = data.CategoryId;
            item.CategoryName = data.CategoryName;
            item.SecondCategoryId = data.SecodCategoryId;
            item.SecondCategoryName = data.SecodCategoryName;
            item.MaterialNo = batchItem.MaterialNo;
            item.MaterialName = batchItem.MaterialName;
            item.Space = batchItem.Space;
            item.NeedMeasure = batchItem.NeedMeasure;
            item.NeedOrder = batchItem.NeedOrder;
            if (batchItem.NeedMeasure)
            {
                item.Status = EnumMaterialItemStatus.ToBeMeasure1;
                item.MeasureStatus = false;
                item.OrderStatus = false;
            }
            else if (!batchItem.NeedMeasure && batchItem.NeedOrder)
            {
                item.Status = EnumMaterialItemStatus.ToSubmitOrder2;
                item.MeasureStatus = true;
                item.OrderStatus = false;
            }
            else if (!batchItem.NeedMeasure && !batchItem.NeedOrder)
            {
                item.Status = EnumMaterialItemStatus.ToBeOutStorageApply6;
                item.MeasureStatus = true;
                item.OrderStatus = true;
            }

            item.Unit = batchItem.Unit;
            item.Quantity = batchItem.Quantity;
            item.Spec = batchItem.Spec;
            item.ComprePrice = batchItem.ComprePrice;
            item.MaterialPrice = batchItem.MaterialPrice;
            item.DetailsId = batchItem.DetailsId;
            item.RowType = batchItem.RowType;

            // 除了无需测量和下单的逻辑外，其余所有任务状态为未开始
            item.ConfirmOrderStatus = false;
            item.DeliveryStatus = false;
            item.InStorageStatus = false;
            item.OutStorageApplyStatus = false;
            item.ConfirmReceiveStatus = false;
            item.InstallStatus = false;
            item.SettlementStatus = false;

            return item;
        }

        public static T_ProjectMaterialItem UpdateMaterialItemSync(T_ProjectMaterialItem item, BatchSyncMaterialItem batchItem)
        {
            item.Space = batchItem.Space;
            item.Unit = batchItem.Unit;
            // 剩余可下单数量，未进入测量状态，所以直接取总数量即可
            item.Quantity = batchItem.Quantity;
            item.Spec = batchItem.Spec;
            item.ComprePrice = batchItem.ComprePrice;
            item.MaterialPrice = batchItem.MaterialPrice;
            item.DetailsId = batchItem.DetailsId;
            item.RowType = batchItem.RowType;


            return item;
        }

        public static T_ProjectMaterialItem MainMaterialMeasure(T_ProjectMaterialItem material)
        {
            material.EditTime = DateTime.Now;
            return material;
        }
    }

    public enum EnumUpdateMaterialType
    {
        /// <summary>
        /// 测量
        /// </summary>
        [Description("测量")]
        Measure1 = 1,

        /// <summary>
        /// 下单
        /// </summary>
        [Description("下单")]
        SubmitOrder2 = 2,

        /// <summary>
        /// 订单确认
        /// </summary>
        [Description("订单确认")]
        ConfirmOrder3 = 3,

        /// <summary>
        /// 总部发货（地采没有该状态）
        /// </summary>
        [Description("总部发货")]
        Delivery4 = 4,

        /// <summary>
        /// 分公司入库（地采没有该状态）
        /// </summary>
        [Description("分公司入库")]
        InStorage5 = 5,

        /// <summary>
        /// 出库申请（地采没有该状态）
        /// </summary>
        [Description("出库申请")]
        OutStorageApply6 = 6,

        /// <summary>
        /// 确认收货
        /// </summary>
        [Description("确认收货")]
        ConfirmReceive7 = 7,

        /// <summary>
        /// 安装
        /// </summary>
        [Description("安装")]
        Install8 = 8,

        /// <summary>
        /// 工费结算
        /// </summary>
        [Description("工费结算")]
        Settlement9 = 9

    }

    public class MaterialDate
    {
        //public bool NeedMeasure { get; set; }
        //public bool NeedOrder { get; set; }

        /// <summary>
        /// 注意：地采没有分类
        /// </summary>
        public int CategoryId { get; set; } = 0;

        /// <summary>
        /// 地采没有分类名称
        /// </summary>
        public string CategoryName { get; set; } = "";

        public int SecodCategoryId { get; set; }

        public string SecodCategoryName { get; set; }
    }
}
