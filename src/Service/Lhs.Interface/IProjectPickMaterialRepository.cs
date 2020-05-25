using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 领料
    /// </summary>
    public interface IProjectPickMaterialRepository : IPlatformBaseService<T_ProjectPickMaterial>
    {
        Task<List<T_ProjectPickMaterial>> GetProjectPickMaterialListByProjectId(int projectId);

        /// <summary>
        /// 提交领料申请
        /// </summary>
        Task<bool> Pick(
            IProjectMaterialRepository projectMaterialRepository,
            IProjectMaterialItemLogRepository logRepository,
            T_ProjectPickMaterial projectPickMaterial,
            T_ProjectMaterialItem material,
            string timeSign,
            string key);

        /// <summary>
        /// 
        /// </summary>
        Task<bool> ConfirmReceive(
            IProjectMaterialRepository projectMaterialRepository,
            IProjectMaterialItemLogRepository logRepository,
            List<T_ProjectMaterialItem> materialList,
            List<T_ProjectPickMaterial> pickMaterialList,
            string timeSign,
            string key);
    }
}
