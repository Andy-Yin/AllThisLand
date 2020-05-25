using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    public interface IUserRepository : IPlatformBaseService<T_User>
    {
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        Task<List<T_User>> GetUserInfo(List<int> userIds);

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        Task<T_User> GetUserInfo(string phone);

        Task<T_User> GetUserById(int id);

        /// <summary>
        /// 根据U9UserId获取用户信息
        /// </summary>
        Task<T_User> GetUserInfoByU9UserId(string userId);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        Task<PageResponse<UserInfo>> GetUserList(string regionName, string companyId, string departmentId
           , int positionId, string searchKey, int pageNum);

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <param name="operateId">操作人</param>
        /// <returns></returns>
        Task<ValidMsgModel> ModifyPassword(int userId, string oldPwd, string newPwd, int operateId);

        /// <summary>
        /// 平登录
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        Task<UserInfo> Login(string phone, string password);

        /// <summary>
        /// 获取用户的审核消息
        /// </summary>
        Task<List<UserApproveMessage>> GetUserApproveMessageList(int userId);
    }
}
