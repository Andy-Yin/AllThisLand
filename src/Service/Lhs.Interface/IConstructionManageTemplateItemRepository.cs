using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ConstructionManageTemplateItem的接口
    ///</summary>
    public interface IConstructionManageTemplateItemRepository : IPlatformBaseService<T_ConstructionManageTemplateItem>
    {
        /// <summary>
        /// 获取模板下的数据
        /// </summary>
        Task<List<T_ConstructionManageTemplateItem>> GetTemplateItemList(int templateId);

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
