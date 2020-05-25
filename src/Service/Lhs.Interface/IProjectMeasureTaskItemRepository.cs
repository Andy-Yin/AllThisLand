using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity;

namespace Lhs.Interface
{
    /// <summary>
    /// 项目主材测量明细任务
    /// </summary>
    public interface IProjectMeasureTaskItemRepository : IPlatformBaseService<T_ProjectMeasureTaskItem>
    {
        Task<List<T_ProjectMeasureTaskItem>> GetTaskItemListByTaskId(int taskId);

    }
}
