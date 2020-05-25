using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    public class ProjectUserFlowPositionRepository : PlatformBaseService<T_ProjectUserFlowPosition>, IProjectUserFlowPositionRepository
    {
        public ProjectUserFlowPositionRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectUserFlowPosition>> GetProjectUserFlowPositionListByUserIdAndType(int userId, int type)
        {
            string sql = @$"select *  from T_ProjectUserFlowPosition where FlowPositionId = {type} and UserId = {userId}";
            var list = (await this.AllAsync(sql)).ToList();
            return list;
        }
    }
}
