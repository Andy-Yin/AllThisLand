using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi
{
    /// <summary>
    /// 领域对象
    /// </summary>
    public class ProjectMaterial
    {
        public ProjectMaterial()
        {

        }

        public bool Add()
        {
            return true;
        }

        public bool Delete()
        {
            return true;
        }

        public bool Update()
        {
            return true;
        }

        public bool SubmitOrder()
        {
            return true;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 物料类型：1 主材 2 地采
        /// </summary>
        public EnumMaterialType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 二级分类Id，也就是second categoryId
        /// </summary>
        public int SecondCategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SecondCategoryName { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialNo { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料空间
        /// </summary>
        public string Space { get; set; }

        /// <summary>
        /// 物料状态：
        /// 如果是主材：1 待测量 2 待下单 3 待订单确认 4 已下单 5 待发货 6 待入库 7 待出库申请 8 待确认收货 9 待安装 10 待工费结算
        /// 如果是地采：1 待测量 2 待下单 3 待订单确认 4 已下单                                8 待确认收货 9 待安装 10 待工费结算
        /// </summary>
        public EnumMaterialItemStatus Status { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 最终数量，提交测量任务后，不可修改！！！
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 费用
        /// </summary>
        public double ComprePrice { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public double MaterialPrice { get; set; }

        /// <summary>
        /// 行Id
        /// </summary>
        public string DetailsId { get; set; }

        /// <summary>
        /// 物料分类 材料或是定额
        /// </summary>
        public string RowType { get; set; }

        /// <summary>
        /// 是否需要测量
        /// </summary>
        public bool NeedMeasure { get; set; }

        /// <summary>
        /// 是否需要下单
        /// </summary>
        public bool NeedOrder { get; set; }

        /// <summary>
        /// 剩余可下单数量
        /// </summary>
        public double RemainingOrderQuantity { get; set; }

        /// <summary>
        /// 剩余可领料数量，当状态进入待出库申请后，更新自U9
        /// 领料后，减掉相应的数量
        /// </summary>
        public double RemainingPickQuantity { get; set; }
    }
}
