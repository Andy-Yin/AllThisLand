using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;

namespace Lhs.Interface
{
    ///<summary>
    ///项目施工管理
    ///</summary>
    public interface IProjectConstructionRepository : IPlatformBaseService<T_ProjectConstructionManage>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_ProjectConstructionManage>> GetList();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);

        /// <summary>
        /// 获取项目下的施工管理基础数据
        /// </summary>
        Task<List<ProjectConstructionManage>> GetProjectConstructionManageList(int projectId);
    }
}
