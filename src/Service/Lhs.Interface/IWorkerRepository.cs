using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Interface
{
    public interface IWorkerRepository : IPlatformBaseService<T_Worker>
    {
        /// <summary>
        /// 获取工人变更列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="type">0 全部 1 施工工人 2 安装工人</param>
        /// <param name="pageNum">第几页</param>
        Task<PageResponse<WorkerChangeInfo>> GetAssignWorkerList(int projectId, int type
            , int pageNum, string minDate, string maxDate, string searchKey);

        /// <summary>
        /// 获取工人列表
        /// </summary>
        Task<List<WorkerInfo>> GetWorkerList(string companyId, int workType, string searchKey);

        /// <summary>
        /// 获取工人信息
        /// </summary>
        Task<WorkerInfo> GetWorkerInfo(int workerId);

        /// <summary>
        /// 获取项目的工人列表
        /// </summary>
        Task<List<WorkerInfo>> GetWorkerList(int projectId, int type);

        /// <summary>
        /// 删除工人
        /// </summary>
        /// <param name="ids">要删除的id </param>
        Task<bool> DeleteWorker(List<int> ids);

    }
}
