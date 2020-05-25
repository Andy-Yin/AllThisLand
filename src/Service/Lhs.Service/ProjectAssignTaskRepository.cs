using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    /// <summary>
    /// 派工任务
    /// </summary>
    public class ProjectAssignTaskRepository : PlatformBaseService<T_ProjectAssignTask>, IProjectAssignTaskRepository
    {
        public ProjectAssignTaskRepository(IConfiguration config)
        {
            Config = config;
        }
        public async Task<List<T_ProjectAssignTask>> GetAssignTaskListByProjectAndUserId(int projectId, int constructionManager)
        {
            string sql = @$"select *  from T_ProjectAssignTask where ProjectId = {projectId} and ConstructionManager = {constructionManager} and IsDel = 0";
            var list = (await this.AllAsync(sql)).ToList();
            return list;
        }
    }
}
