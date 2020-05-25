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
    public interface IStopWorkReasonRepository : IPlatformBaseService<T_StopWorkReason>
    {
        /// <summary>
        /// 获取停复工原因
        /// </summary>
        Task<List<T_StopWorkReason>> GetStopWorkReasons();

        /// <summary>
        /// 删除停复工原因
        /// </summary>
        /// <param name="ids">要删除的id </param>
        Task<bool> DeleteWorkType(List<int> ids);
    }
}
