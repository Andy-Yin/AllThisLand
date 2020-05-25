using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 安装任务
    /// </summary>
    public interface IProjectInstallTaskRepository : IPlatformBaseService<T_ProjectInstallTask>
    {
        /// <summary>
        /// 获取某个项目某个主材下，所有的安装任务列表，不分页，外部分页
        /// </summary>
        Task<List<T_ProjectInstallTask>> GetInstallTaskList(int projectId, int secondCategoryId);

        /// <summary>
        /// 获取某个项目，某个工长下所有的安装任务
        /// </summary>
        Task<List<T_ProjectInstallTask>> GetInstallTaskListByProjectAndUserId(int projectId, int userId);

        /// <summary>
        /// 提交任务
        /// </summary>
        Task<bool> SubmitTask(
            List<T_ProjectInstallTask> taskList,
            IProjectMaterialItemLogRepository logRepository,
            IProjectMaterialRepository projectMaterialRepository,
            string timeSign,
            string key);
    }
}
