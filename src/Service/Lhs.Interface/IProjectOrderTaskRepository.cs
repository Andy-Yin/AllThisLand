using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 项目主材下单任务
    /// </summary>
    public interface IProjectOrderTaskRepository : IPlatformBaseService<T_ProjectOrderTask>
    {
        /// <summary>
        /// 获取某个项目，某个二级分类下，所有的订单任务列表，不分页，外部分页
        /// </summary>
        /// <returns></returns>
        Task<List<T_ProjectOrderTask>> GetOrderTaskList(int projectId, int secondCategoryId);

        /// <summary>
        /// 获取某个项目下所有的订单任务
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<List<T_ProjectOrderTask>> GetOrderTaskListByProjectId(int projectId);

        /// <summary>
        /// 获取某个项目，某个工长下所有的订单任务
        /// </summary>
        Task<List<T_ProjectOrderTask>> GetOrderTaskListByProjectAndUserId(int projectId, int userId);

        /// <summary>
        /// 批量提交提交订单任务
        /// </summary>
        Task<bool> SubmitOrderTask(
            string projectId,
            string remark,
            int orderType,
            int supplier,
            List<T_ProjectOrderTask> taskList,
            IProjectMaterialItemLogRepository logRepository,
            IProjectMaterialRepository projectMaterialRepository,
            string timeSign,
            string key,
            int userId);

        /// <summary>
        /// 批量更新订单任务，包含总部发货，分公司入库，出库三种操作
        /// </summary>
        Task<bool> BatchUpdateOrderFromU9(
            List<T_ProjectOrderTask> taskList,
            IProjectMaterialRepository projectMaterialRepository,
            IProjectMaterialItemLogRepository logRepository,
            List<T_ProjectMaterialItem> materialList);
    }
}
