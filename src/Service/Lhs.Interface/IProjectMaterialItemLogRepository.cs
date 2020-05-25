using Lhs.Entity.DbEntity.DbModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 项目物料管理变化日志
    /// </summary>
    public interface IProjectMaterialItemLogRepository : IPlatformBaseService<T_ProjectMaterialItemLog>
    {
        /// <summary>
        /// 获取某个物料的所有状态记录
        /// </summary>
        /// <param name="itemId">物料Id</param>
        /// <returns></returns>
        Task<List<T_ProjectMaterialItemLog>> GetLogListByItemId(int itemId);
    }
}
