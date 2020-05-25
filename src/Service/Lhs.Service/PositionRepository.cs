using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using NPOI.HSSF.Model;

namespace Lhs.Service
{
    /// <summary>
    /// 岗位
    /// </summary>
    public class PositionRepository : PlatformBaseService<T_Position>, IPositionRepository
    {
        public PositionRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有岗位
        /// </summary>
        public async Task<List<T_Position>> GetAllPosition()
        {
            using (var coon = Connection)
            {
                var querySql = @";SELECT * FROM dbo.T_Position WHERE IsDel=0";
                return (await coon.QueryAsync<T_Position>(querySql)).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_Position SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        public async Task<List<T_Menu>> GetAllMenu()
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT * FROM dbo.T_Menu WITH (NOLOCK) WHERE IsDel=0";
                return (await coon.QueryAsync<T_Menu>(sql)).ToList();

            }
        }

        /// <summary>
        /// 获取某个岗位的权限
        /// </summary>
        public async Task<List<int>> GetPositionPermission(int position)
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT DISTINCT MenuId FROM dbo.T_PositionPermission WITH (NOLOCK) WHERE IsDel=0 AND PositionId=@position";
                return (await coon.QueryAsync<int>(sql, new { position })).ToList();
            }
        }

        /// <summary>
        /// 获取用户的权限
        /// </summary>
        public async Task<List<int>> GetUserPermission(int userId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    SELECT DISTINCT
                           pp.MenuId
                    FROM dbo.T_UserPosition up WITH (NOLOCK)
                        INNER JOIN dbo.T_Position p WITH (NOLOCK)
                            ON up.IsDel = 0
                               AND up.UserId = @userId
                               AND up.PositionId = p.Id
                               AND p.IsDel = 0
                        INNER JOIN dbo.T_PositionPermission pp WITH (NOLOCK)
                            ON p.Id = pp.PositionId
                               AND pp.IsDel = 0;";
                return (await coon.QueryAsync<int>(sql, new { userId })).ToList();
            }
        }

        /// <summary>
        /// 保存岗位权限
        /// </summary>
        public async Task<bool> SavePositionPermission(int positionId, List<int> menuIds)
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT * FROM dbo.T_PositionPermission p WITH(NOLOCK) WHERE p.PositionId=@positionId AND p.IsDel=0";
                var positionPermission = (await coon.QueryAsync<T_PositionPermission>(sql, new { positionId })).ToList();
                var dbMenuIds = positionPermission.Where(n => n.PositionId == positionId).Select(n => n.MenuId).Distinct().ToList();
                var toAddMenuIds = menuIds.Except(dbMenuIds).ToList();
                var toDeleteMenuIds = dbMenuIds.Except(menuIds).ToList();
                sql = string.Empty;
                if (toDeleteMenuIds.Any())
                {
                    sql += $@";
                    UPDATE dbo.T_PositionPermission SET IsDel=1,EditTime=GETDATE() WHERE PositionId=@positionId AND IsDel=0 AND MenuId IN ({string.Join(",", toDeleteMenuIds)});";
                }
                if (toAddMenuIds.Any())
                {
                    sql += $@";
                    INSERT INTO dbo.T_PositionPermission
                    (
                        PositionId,
                        MenuId
                    )
                    SELECT @positionId,
                           b.Id
                    FROM dbo.T_Menu b WITH (NOLOCK)
                    WHERE b.Id IN ( {string.Join(",", toAddMenuIds)} );";
                }

                if (string.IsNullOrEmpty(sql))
                {
                    return true;
                }

                var result = await coon.ExecuteAsync(sql, new { positionId });
                return result > 0;
            }
        }

        /// <summary>
        /// 保存用户的岗位
        /// </summary>
        public async Task<bool> SaveUserPosition(int userId, List<int> positionIds)
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT DISTINCT p.PositionId FROM dbo.T_UserPosition p WITH(NOLOCK) WHERE p.UserId=@userId AND p.IsDel=0";
                var userPositions = (await coon.QueryAsync<int>(sql, new { userId })).ToList();
                var toAddIds = positionIds.Except(userPositions).ToList();
                var toDeleteIds = userPositions.Except(positionIds).ToList();
                sql = string.Empty;
                if (toDeleteIds.Any())
                {
                    sql = $@";
                    UPDATE dbo.T_UserPosition SET IsDel=1,EditTime=GETDATE() WHERE UserId=@userId AND PositionId IN ({string.Join(",", toDeleteIds)});";
                }
                if (toAddIds.Any())
                {
                    sql = $@";
                    INSERT INTO dbo.T_UserPosition
                    (
                        UserId,
                        PositionId
                    )
                    SELECT @userId,
                           b.Id
                    FROM dbo.T_Position b WITH (NOLOCK)
                    WHERE b.Id IN ( {string.Join(",", toAddIds)} );";
                }
                if (string.IsNullOrEmpty(sql))
                {
                    return true;
                }

                var result = await coon.ExecuteAsync(sql, new { userId });
                return result > 0;
            }
        }
    }
}
