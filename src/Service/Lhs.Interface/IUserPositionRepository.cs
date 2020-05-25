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
    public interface IUserPositionRepository : IPlatformBaseService<T_UserPosition>
    {
        /// <summary>
        /// 获取用户的岗位
        /// </summary>
        Task<List<T_UserPosition>> GetUserPositions(int userId);

        /// <summary>
        /// 获取用户的岗位
        /// </summary>
        Task<List<UserPosition>> GetUserPositions(List<int> userIds);

        /// <summary>
        /// 删除工人变更
        /// </summary>
        /// <param name="ids">要删除的id </param>
        Task<bool> DeleteChange(List<int> ids);
    }
}
