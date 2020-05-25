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
    public interface IPositionRepository : IPlatformBaseService<T_Position>
    {
        /// <summary>
        /// 获取所有岗位
        /// </summary>
        Task<List<T_Position>> GetAllPosition();

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        Task<List<T_Menu>> GetAllMenu();

        /// <summary>
        /// 获取某个岗位的权限
        /// </summary>
        Task<List<int>> GetPositionPermission(int position);

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        Task<List<int>> GetUserPermission(int userId);

        /// <summary>
        /// 保存岗位权限
        /// </summary>
        Task<bool> SavePositionPermission(int positionId, List<int> menuIds);

        /// <summary>
        /// 保存用户的岗位
        /// </summary>
        Task<bool> SaveUserPosition(int userId, List<int> positionIds);
    }
}
