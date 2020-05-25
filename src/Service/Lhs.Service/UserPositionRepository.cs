using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace Lhs.Service
{
    /// <summary>
    /// 用户岗位
    /// </summary>
    public class UserPositionRepository : PlatformBaseService<T_UserPosition>, IUserPositionRepository
    {
        public UserPositionRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取用户的岗位
        /// </summary>
        public async Task<List<T_UserPosition>> GetUserPositions(int userId)
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_UserPosition WHERE UserId=@userId AND IsDel=0";
                return (await coon.QueryAsync<T_UserPosition>(sql, new { userId })).ToList();
            }
        }

        /// <summary>
        /// 获取用户的岗位
        /// </summary>
        public async Task<List<UserPosition>> GetUserPositions(List<int> userIds)
        {
            using (var coon = Connection)
            {
                var sql = $@"
                    SELECT DISTINCT
                           up.UserId,
                           p.Id AS PositionId,
                           p.Name AS PositionName
                    FROM dbo.T_UserPosition up WITH (NOLOCK)
                        INNER JOIN dbo.T_Position p WITH (NOLOCK)
                            ON up.IsDel = 0
                               AND up.UserId IN ( {string.Join(',', userIds)} )
                               AND up.PositionId = p.Id
                               AND p.IsDel = 0
                    ORDER BY up.UserId,
                             p.Id,
                             p.Name;";
                return (await coon.QueryAsync<UserPosition>(sql)).ToList();
            }
        }

        /// <summary>
        /// 删除工人变更
        /// </summary>
        /// <param name="ids">要删除的id </param>
        public async Task<bool> DeleteChange(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectWorkerChangeRecord SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
