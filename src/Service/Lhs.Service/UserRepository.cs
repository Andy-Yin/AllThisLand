using Core.Data;
using Core.Util.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Entity.ForeignDtos.Request.User;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public class UserRepository : PlatformBaseService<T_User>, IUserRepository
    {
        public UserRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        public async Task<List<T_User>> GetUserInfo(List<int> userIds)
        {
            using (var coon = Connection)
            {
                var querySql = $@";
                SELECT *
                FROM dbo.T_User WITH(NOLOCK)
                WHERE Id IN({string.Join(',', userIds)})
                AND IsDel = 0; ";
                return (await coon.QueryAsync<T_User>(querySql)).ToList();
            }
        }

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        public async Task<T_User> GetUserInfo(string phone)
        {
            using (var coon = Connection)
            {
                var querySql = $"SELECT * FROM T_User WHERE Phone = @phone AND IsDel = 0";
                var user = await coon.QueryFirstOrDefaultAsync<T_User>(querySql, new { phone });
                return user;
            }
        }

        public async Task<T_User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据U9UserId获取用户信息
        /// </summary>
        public async Task<T_User> GetUserInfoByU9UserId(string userId)
        {
            using (var coon = Connection)
            {
                var querySql = "SELECT * FROM T_User WHERE U9UserId = @userId AND IsDel = 0";
                var user = await coon.QueryFirstOrDefaultAsync<T_User>(querySql, new { userId });
                return user;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public async Task<PageResponse<UserInfo>> GetUserList(string regionName, string companyId, string departmentId
            , int positionId, string searchKey, int pageNum)
        {
            using (var coon = Connection)
            {
                var where = string.Empty;
                if (!string.IsNullOrEmpty(regionName))
                {
                    where += @"
                            AND EXISTS
                            (
                                SELECT 1
                                FROM dbo.T_Company c2 WITH (NOLOCK)
                                WHERE c2.CompanyId = u.CompanyId
                                      AND c2.IsDel = 0
                                      AND c2.RegionName = @regionName
                            )";
                }
                if (!string.IsNullOrEmpty(companyId))
                {
                    where += " AND u.CompanyId = @companyId";
                }
                if (!string.IsNullOrEmpty(departmentId))
                {
                    where += " AND u.DepartmentId = @departmentId";
                }
                if (positionId > 0)
                {
                    where += @"
                            AND EXISTS
                            (
                                SELECT 1
                                FROM dbo.T_UserPosition up WITH (NOLOCK)
                                WHERE up.UserId = u.Id
                                      AND up.IsDel = 0
                                      AND up.PositionId = @positionId
                            )";
                }
                if (!string.IsNullOrEmpty(searchKey))
                {
                    where += @"
                          AND
                          (
                              u.Name LIKE @searchKey
                              OR u.Phone LIKE @searchKey
                          )";
                }

                var sql = $@"
                    SELECT *,
                           (
                               SELECT TOP 1
                                      Name
                               FROM dbo.T_Company c WITH (NOLOCK)
                               WHERE c.CompanyId = u.CompanyId
                                     AND c.IsDel = 0
                           ) AS CompanyName,
                           (
                               SELECT TOP 1
                                      RegionName
                               FROM dbo.T_Company c WITH (NOLOCK)
                               WHERE c.CompanyId = u.CompanyId
                                     AND c.IsDel = 0
                           ) AS RegionName,
                           (
                               SELECT TOP 1
                                      Name
                               FROM dbo.T_Department d WITH (NOLOCK)
                               WHERE d.DepartmentId = u.DepartmentId
                                     AND d.IsDel = 0
                           ) AS DepartmentName
                    FROM dbo.T_User u WITH (NOLOCK)
                    WHERE u.IsDel = 0
                          {where}";
                return await PagedAsync<UserInfo>(sql, new
                {
                    regionName,
                    companyId,
                    departmentId,
                    positionId,
                    searchKey = $"%{searchKey}%"
                }, "", pageNum, CommonConst.PageSize);
            }
        }

        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd">新密码</param>
        /// <param name="operateId">操作人</param>
        /// <returns></returns>
        public async Task<ValidMsgModel> ModifyPassword(int userId, string oldPwd, string newPwd, int operateId)
        {
            var result = new ValidMsgModel { Success = false, Msg = string.Empty };
            using (var coon = Connection)
            {
                var querySql = $"SELECT * FROM T_User WHERE Id = @userId AND Password = @oldPwd AND IsDel = 0";
                var user = await coon.QueryFirstOrDefaultAsync<T_User>(querySql, new { userId, oldPwd });
                if (user == null)
                {
                    result.Msg = CommonMessage.OldPasswordError;
                    return result;
                }
                user.Password = newPwd;
                user.EditTime = DateTime.Now;
                var status = await UpdateAsync(user);
                if (status)
                {
                    result.Success = true;
                    return result;
                }
                result.Msg = "更改密码失败";
                return result;
            }
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="password">密码</param>
        public async Task<UserInfo> Login(string phone, string password)
        {
            var sql = @";
                SELECT u.*,
                       c.Name AS CompanyName,
                       d.Name AS DepartmentName
                FROM dbo.T_User u WITH (NOLOCK)
                    LEFT JOIN dbo.T_Company c WITH (NOLOCK)
                        ON u.CompanyId = c.CompanyId
                           AND c.IsDel = 0
                    LEFT JOIN dbo.T_Department d WITH (NOLOCK)
                        ON u.DepartmentId = d.DepartmentId
                           AND d.IsDel = 0
                WHERE u.Phone = @phone
                      AND u.Password = @password
                      AND u.IsDel = 0
                      AND u.IsUsed = 1;";
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<UserInfo>(sql, new { phone, password });
            }
        }

        /// <summary>
        /// 获取用户的审核消息
        /// </summary>
        public async Task<List<UserApproveMessage>> GetUserApproveMessageList(int userId)
        {
            using (var coon = Connection)
            {
                var sql = @"
                    SELECT *
                    FROM
                    (
                        SELECT p.Id AS ProjectId,
                               p.QuotationId,
                               p.ProjectName,
                               (
                                   SELECT TOP (1)
                                          record.CreateTime
                                   FROM dbo.T_ProjectFlowRecord record WITH (NOLOCK)
                                   WHERE record.ProjectId = p.Id
                                   ORDER BY record.Id DESC
                               ) AS CreateTime,
                               flow.Type,
                               0 AS Status
                        FROM dbo.T_ProjectUserFlowPosition position WITH (NOLOCK)
                            INNER JOIN dbo.T_Project p WITH (NOLOCK)
                                ON position.ProjectId = p.Id
                                   AND position.UserId = @userId
                                   AND position.FlowPositionId != 9 -- 不计算客户
                                   AND p.IsDel = 0
                                   AND p.FollowId >= 2
                            INNER JOIN dbo.T_FlowNode flow WITH (NOLOCK)
                                ON p.FollowId = flow.Id
                                   AND position.FlowPositionId = flow.FlowPositionId
                                   AND flow.PreNodeId > 0 -- 只展示审核步骤，不展示提交审核的步骤
                                   AND flow.IsDel = 0
                    ) a
                    ORDER BY a.CreateTime DESC;";
                return (await coon.QueryAsync<UserApproveMessage>(sql, new { userId })).ToList();
            }
        }
    }
}
