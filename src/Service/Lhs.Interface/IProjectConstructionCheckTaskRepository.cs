using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ProjectConstructionCheckTask的接口
    ///</summary>
    public interface IProjectConstructionCheckTaskRepository : IPlatformBaseService<T_ProjectConstructionCheckTask>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ProjectConstructionCheckTask>> GetList();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
