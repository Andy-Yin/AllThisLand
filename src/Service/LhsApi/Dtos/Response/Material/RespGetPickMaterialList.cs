using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Material
{
    public class RespGetPickMaterialList
    {
        public RespGetPickMaterialList(T_ProjectPickMaterial item)
        {
            PickNo = item.PickNo;
            MaterialNo = item.MaterialNo;
            MaterialName = item.MaterialName;
            Space = item.Space;
            Quantity = item.Quantity;
            Amount = item.Amount;
            Status = item.Status;
            Remark = item.Remark;
        }
        /// <summary>
        /// 领料单号
        /// </summary>
        public string PickNo { get; set; }

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
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 领料状态：1 申请中 2 已完成
        /// </summary>
        public EnumPickMaterialStatus Status { get; set; }
    }

    public class PickMaterialItemList
    {
        public PickMaterialItemList(int status, string statusName)
        {
            Status = status;
            StatusName = statusName;
            ItemList = new ArrayList();
        }

        /// <summary>
        /// 1-申请中， 2-已完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 1-申请中，2-已完成
        /// </summary>
        public string StatusName { get; set; }

        public ArrayList ItemList { get; set; }
    }
}
