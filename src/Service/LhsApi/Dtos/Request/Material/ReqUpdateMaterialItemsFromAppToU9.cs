using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqUpdateMaterialItemsFromAppToU9
    {
        public List<UpdateMaterialItemFromAppToU9> MaterialList { get; set; }
    }

    /// <summary>
    /// 用于调用U9系统的参数
    /// </summary>
    public class UpdateMaterialItemFromAppToU9
    {
        /// <summary>
        /// APP系统项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 【APP接口方存储的】主材明细的Id，如某一种瓷砖的Id
        /// </summary>
        public int ProjectMaterialId { get; set; }

        /// <summary>
        /// 调用U9传的物料状态
        /// </summary>
        public EnumUpdateMaterialItemFromAppToU9 Status { get; set; }
    }

    /// <summary>
    /// 调用U9同步物料状态的状态值
    /// </summary>
    public enum EnumUpdateMaterialItemFromAppToU9
    {
        /// <summary>
        /// 下单完成（没有1,1是测量状态，保留）
        /// </summary>
        OrderSubmitted = 2,

        /// <summary>
        /// 出库申请
        /// </summary>
        OutStorageApply = 6,

        /// <summary>
        /// 确认收货
        /// </summary>
        ConfirmReceive = 8,

        /// <summary>
        /// 安装完成
        /// </summary>
        InstallFinished = 9
    }
}
