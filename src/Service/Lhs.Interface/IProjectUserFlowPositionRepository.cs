using Core.Data;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Entity.ForeignDtos.Response.Project;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    public interface IProjectUserFlowPositionRepository : IPlatformBaseService<T_ProjectUserFlowPosition>
    {
        /// <summary>
        /// 判断是否是工长还是监理
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="type">T_FlowPosition里的数值 1-工长，4-监理</param>
        /// <returns></returns>
        Task<List<T_ProjectUserFlowPosition>> GetProjectUserFlowPositionListByUserIdAndType(int userId, int type);
    }
}
