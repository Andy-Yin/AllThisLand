using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 派工任务
    /// </summary>
    public interface IProjectAssignTaskRepository : IPlatformBaseService<T_ProjectAssignTask>
    {
        /// <summary>
        /// 获取某个项目，某个工长下所有的派工任务
        /// </summary>
        /// <param name="projectId">项目Id</param>
        /// <param name="constructionManager">工长Id</param>
        /// <returns></returns>
        Task<List<T_ProjectAssignTask>> GetAssignTaskListByProjectAndUserId(int projectId, int constructionManager);
    }
}
