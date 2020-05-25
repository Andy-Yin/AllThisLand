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
using Dapper.Contrib.Extensions;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 交底基础数据
    /// </summary>
    public class DisclosureItemRepository : PlatformBaseService<T_DisclosureItem>, IDisclosureItemRepository
    {
        public DisclosureItemRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取交底基础数据
        /// </summary>
        /// <param name="type">0 预交底 1 交底</param>
        /// <param name="name">搜索条件：名称</param>
        public async Task<List<T_DisclosureItem>> GetDisclosureItemList(int type, string name)
        {
            using (var coon = Connection)
            {
                string sql;
                if (string.IsNullOrEmpty(name))
                {
                    sql = "SELECT * FROM dbo.T_DisclosureItem WITH (NOLOCK) WHERE Type=@type AND IsDel=0";
                    return (await coon.QueryAsync<T_DisclosureItem>(sql, new { type })).ToList();
                }
                sql = "SELECT * FROM dbo.T_DisclosureItem WITH (NOLOCK) WHERE Type=@type AND IsDel=0 AND Name LIKE @name";
                return (await coon.QueryAsync<T_DisclosureItem>(sql, new { type, name = $"%{name}%" })).ToList();
            }
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：1 预交底 2交底</param>
        public async Task<bool> DeleteDisclosureItem(List<int> ids, int type)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_DisclosureItem SET IsDel=1,EditTime=GETDATE() WHERE Type=@type AND Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql, new { type = type - 1 });
                return result > 0;
            }
        }

        /// <summary>
        /// 删除项目中的基础数据
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：1 预交底 2交底</param>
        /// <param name="projectId">项目id</param>
        public async Task<bool> DeleteProjectDisclosureItem(List<int> ids, int type, int projectId)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ProjectDisclosure SET IsDel=1,EditTime=GETDATE() WHERE Type=@type AND ProjectId=@projectId AND Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql, new { type = type - 1, projectId });
                return result > 0;
            }
        }
    }
}
