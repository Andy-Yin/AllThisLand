using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ConstructionPlanStage的接口
    ///</summary>
    public interface IConstructionPlanStageRepository : IPlatformBaseService<T_ConstructionPlanStage>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ConstructionPlanStage>> GetList();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
