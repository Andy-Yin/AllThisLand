using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///T_ProjectMaterialLabour
    ///</summary>
    public interface IProjectMaterialLabourRepository : IPlatformBaseService<T_ProjectMaterialLabour>
    {
        Task<List<T_ProjectMaterialLabour>> GetListByProjectId(int projectId);
    }
}
