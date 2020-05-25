using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ProjectPlanStage的接口
    ///</summary>
    public interface IProjectPlanStageRepository : IPlatformBaseService<T_ProjectPlanStage>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ProjectPlanStage>> GetList();

        /// <summary>
        /// 获取项目下的施工计划阶段
        /// </summary>
        Task<List<T_ProjectPlanStage>> GetProjectPlanStageList(int projectId);

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
