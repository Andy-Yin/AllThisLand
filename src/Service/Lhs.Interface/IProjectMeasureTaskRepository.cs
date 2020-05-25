using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity;

namespace Lhs.Interface
{
    /// <summary>
    /// 项目主材测量任务
    /// </summary>
    public interface IProjectMeasureTaskRepository : IPlatformBaseService<T_ProjectMeasureTask>
    {
        /// <summary>
        /// 获取某个项目，所有的测量任务列表，不分页，外部分页
        /// </summary>
        Task<List<ProjectMeasureTask>> GetMeasureTaskList(int projectId, int secondCategoryId);

        /// <summary>
        /// 获取某个项目，某个工长下所有测量任务
        /// </summary>
        Task<List<T_ProjectMeasureTask>> GetMeasureTaskListByProjectAndUserId(int projectId, int userId);

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="projectMeasureTaskItemRepository"></param>
        /// <param name="taskItemList"></param>
        /// <param name="projectMaterialRepository"></param>
        /// <param name="timeSign"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> SubmitTask(
            List<T_ProjectMeasureTask> taskList,
            List<SubMeasureTaskItem> taskItemList,
            IProjectMaterialItemLogRepository logRepository,
            IProjectMeasureTaskItemRepository projectMeasureTaskItemRepository,
            IProjectMaterialRepository projectMaterialRepository,
            string timeSign,
            string key);
    }
}
