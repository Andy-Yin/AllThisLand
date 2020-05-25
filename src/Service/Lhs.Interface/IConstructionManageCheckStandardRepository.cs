using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ConstructionManageCheckStandard的接口
    ///</summary>
    public interface IConstructionManageCheckStandardRepository : IPlatformBaseService<T_ConstructionManageCheckStandard>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ConstructionManageCheckStandard>> GetList();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
