using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    /// <summary>
    /// 跟进日志相关
    /// </summary>
    public class FollowUpLogRepository : PlatformBaseService<T_FollowupLog>, IFollowUpLogRepository
    {
        public FollowUpLogRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<bool> AddFollowUpLog(T_FollowupLog log)
        {
            using (var conn = Connection)
            {
                return (await conn.InsertAsync<T_FollowupLog>(log) > 0);
            }
        }

        public async Task<List<T_FollowupType>> GetFollowUpTypeList()
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_FollowupType WHERE IsDel=0 ");
                return (await conn.QueryAsync<T_FollowupType>(sql)).ToList();
            }
        }

        public async Task<List<T_FollowupLog>> GetFollowUpLogList(int userId, int projectId)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_FollowupLog WHERE IsDel=0 and ProjectId = {0} and UserId = {1}", projectId, userId);
                return (await conn.QueryAsync<T_FollowupLog>(sql)).ToList();
            }
        }

        public async Task<bool> SaveFollowUpLogType(T_FollowupType item)
        {
            using (var conn = Connection)
            {
                if (item.Id > 0)
                {
                    return (await conn.UpdateAsync(item));
                }

                return (await conn.InsertAsync(item)) > 0;
            }
        }

        public async Task<bool> DeleteFollowUpLogType(int id)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"update T_FollowupType set IsDel = 1 where id = {0} ", id);
                return (await conn.ExecuteAsync(sql)) > 0;
            }
        }
    }
}
