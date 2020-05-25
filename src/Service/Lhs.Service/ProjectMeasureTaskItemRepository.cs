using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Lhs.Entity.DbEntity;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    /// <summary>
    /// 测量任务
    /// </summary>
    public class ProjectMeasureTaskItemRepository : PlatformBaseService<T_ProjectMeasureTaskItem>, IProjectMeasureTaskItemRepository
    {
        public ProjectMeasureTaskItemRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectMeasureTaskItem>> GetTaskItemListByTaskId(int taskId)
        {
            var sql = $"select * from T_ProjectMeasureTaskItem where taskId={taskId} and isdel = 0";
            return (await this.AllAsync(sql)).ToList();
        }

    }
}
