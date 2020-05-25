using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ProjectConstructionCheckStandard的接口
    ///</summary>
    public interface IProjectConstructionCheckStandardRepository : IPlatformBaseService<T_ProjectConstructionCheckStandard>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ProjectConstructionCheckStandard>> GetList();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
