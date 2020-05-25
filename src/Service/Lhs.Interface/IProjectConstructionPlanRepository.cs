using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Project;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ProjectConstructionPlan的接口
    ///</summary>
    public interface IProjectConstructionPlanRepository : IPlatformBaseService<T_ProjectConstructionPlan>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ProjectConstructionPlan>> GetList();

        /// <summary>
        /// 获取项目下的施工计划基础数据
        /// </summary>
        Task<List<T_ProjectConstructionPlan>> GetProjectConstructionPlanList(int projectId);

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);

        /// <summary>
        /// 获取项目下的施工计划
        /// </summary>
        Task<List<ProjectPlanInfo>> GetProjectPlans(int projectId);
    }
}
