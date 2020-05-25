using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ConstructionManageTemplate的接口
    ///</summary>
    public interface IConstructionManageTemplateRepository : IPlatformBaseService<T_ConstructionManageTemplate>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ConstructionManageTemplate>> GetList();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
