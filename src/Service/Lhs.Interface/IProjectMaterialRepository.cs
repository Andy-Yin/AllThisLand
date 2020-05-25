using Lhs.Entity.DbEntity.DbModel;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 项目物料管理
    /// </summary>
    public interface IProjectMaterialRepository : IPlatformBaseService<T_ProjectMaterialItem>
    {
        /// <summary>
        /// 获取某个项目某个二级分类下，所有的物料明细列表
        /// </summary>
        Task<List<T_ProjectMaterialItem>> GetProjectMaterialItemListByProjectIdAndSecondCategoryId(int projectId, int secondCategoryId, EnumMaterialType type = EnumMaterialType.Main);

        /// <summary>
        /// 【要判断是否为空】根据物料编码和空间获取物料
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="number">物料编码</param>
        /// <param name="space">空间</param>
        /// <returns></returns>
        Task<T_ProjectMaterialItem> GetProjectMaterialItemByNoAndSpace(int projectId, string number, string space);

        /// <summary>
        /// 获取某个项目某一种物料信息
        /// </summary>
        Task<List<T_ProjectMaterialItem>> GetProjectMaterialItemListByProjectId(int projectId, EnumMaterialType type);

        /// <summary>
        /// 获取所有物料
        /// </summary>
        Task<List<T_ProjectMaterialItem>> GetProjectMaterialItemListByProjectId(int projectId);

        /// <summary>
        /// 更新某一条物料明细的状态
        /// </summary>
        /// <param name="itemId">主材明细Id；</param>
        /// <param name="status">状态值。物料状态：1 待测量 2 待下单 3 待订单确认 4 已下单 5 待发货 6 待入库 7 待出库申请 8 待确认收货 9 待安装 10 待工费结算</param>
        /// <returns></returns>
        Task<bool> UpdateMaterialItemStatus(int itemId, EnumMaterialItemStatus status);

        /// <summary>
        /// 同步物料时，批量更新物料（进入测量任务后不可更新）
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncMaterialFromU9(
            IProjectMaterialItemLogRepository logRepository,
            IProjectMaterialLabourRepository labourRepository,
            List<T_ProjectMaterialLabour> labourList,
            List<T_ProjectMaterialItem> insertItems,
            List<T_ProjectMaterialItem> updateItems,
            List<T_ProjectMaterialItem> deleteItems
            );

        /// <summary>
        /// 更新项目所有的物料为工费结算完成
        /// </summary>
        Task<bool> ConfirmSettlementFromU9(int projectId);

        /// <summary>
        /// 删除物料
        /// </summary>
        Task<bool> DeleteMaterial(
            int materialId,
            IProjectMeasureTaskRepository measureTaskRepository,
            IProjectOrderTaskRepository orderTaskRepository,
            IProjectPickMaterialRepository pickMaterialRepository,
            IProjectInstallTaskRepository installTaskRepository,
            SqlConnection conn,
            SqlTransaction tran
        );
    }
}
