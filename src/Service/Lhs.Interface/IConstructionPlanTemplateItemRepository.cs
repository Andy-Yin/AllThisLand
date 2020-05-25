using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ConstructionPlanTemplateItem的接口
    ///</summary>
    public interface IConstructionPlanTemplateItemRepository : IPlatformBaseService<T_ConstructionPlanTemplateItem>
    {
        /// <summary>
        /// 获取模板下的数据
        /// </summary>
        Task<List<T_ConstructionPlanTemplateItem>> GetTemplateItemList(int templateId);

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
