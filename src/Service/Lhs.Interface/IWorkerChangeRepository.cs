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
    public interface IWorkerChangeRepository : IPlatformBaseService<T_ProjectWorkerChangeRecord>
    {
        /// <summary>
        /// 删除工人变更
        /// </summary>
        /// <param name="ids">要删除的id </param>
        Task<bool> DeleteChange(List<int> ids);

        /// <summary>
        /// 工人变更
        /// </summary>
        Task<bool> ChangeWorker(int projectId, int type, int oldWorkerId, int newWorkerId);
    }
}
