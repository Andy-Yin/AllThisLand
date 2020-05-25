using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    public interface IMainMaterialCategoryRepository : IPlatformBaseService<T_MainMaterialCategory>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_MainMaterialCategory>> GetList();

        /// <summary>
        /// 删除基础数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
