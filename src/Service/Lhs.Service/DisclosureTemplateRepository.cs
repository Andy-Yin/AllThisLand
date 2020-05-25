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
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace Lhs.Service
{
    /// <summary>
    /// 交底模板
    /// </summary>
    public class DisclosureTemplateRepository : PlatformBaseService<T_DisclosureTemplate>, IDisclosureTemplateRepository
    {
        public DisclosureTemplateRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取预交底、交底模板
        /// </summary>
        /// <param name="type">0 预交底 1 交底</param>
        /// <param name="name">搜索条件：名称</param>
        public async Task<List<T_DisclosureTemplate>> GetTemplateList(int type, string name)
        {
            using (var coon = Connection)
            {
                string sql;
                if (string.IsNullOrEmpty(name))
                {
                    sql = "SELECT * FROM dbo.T_DisclosureTemplate WITH (NOLOCK) WHERE Type=@type AND IsDel=0";
                    return (await coon.QueryAsync<T_DisclosureTemplate>(sql, new { type })).ToList();
                }
                sql = "SELECT * FROM dbo.T_DisclosureTemplate WITH (NOLOCK) WHERE Type=@type AND IsDel=0 AND Name LIKE @name";
                return (await coon.QueryAsync<T_DisclosureTemplate>(sql, new { type, name = $"%{name}%" })).ToList();
            }
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：1 预交底 2交底</param>
        public async Task<bool> DeleteTemplate(List<int> ids, int type)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_DisclosureTemplate SET IsDel=1,EditTime=GETDATE() WHERE Type=@type AND Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql, new { type = type - 1 });
                return result > 0;
            }
        }

        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="type">类型：0 预交底 1交底</param>
        /// <param name="templateId">模板id</param>
        public async Task<List<DisclosureItemInfo>> GetTemplateItemList(int type, int templateId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    SELECT di.*,
                           ISNULL(dti.Id, 0) AS TemplateId
                    FROM dbo.T_DisclosureItem di WITH (NOLOCK)
                        LEFT JOIN dbo.T_DisclosureTemplateItem dti WITH (NOLOCK)
                            ON di.Id = dti.ItemId
                               AND dti.IsDel = 0
                               AND dti.TemplateId = @templateId
                    WHERE di.IsDel = 0
                          AND di.Type = @type;";
                return (await coon.QueryAsync<DisclosureItemInfo>(sql, new { type, templateId })).ToList();
            }
        }

        /// <summary>
        /// 获取项目的交底内容
        /// </summary>
        /// <param name="type">类型：0 预交底 1交底</param>
        /// <param name="projectId">项目id</param>
        public async Task<List<DisclosureItemInfo>> GetProjectItemList(int type, int projectId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    SELECT p.Id,
                           p.DisclosureItemName AS Name,
                           p.Remark
                    FROM dbo.T_ProjectDisclosure p WITH (NOLOCK)
                    WHERE p.IsDel = 0
                          AND p.ProjectId = @projectId
                          AND p.Type = @type;";
                return (await coon.QueryAsync<DisclosureItemInfo>(sql, new { type, projectId })).ToList();
            }
        }
    }
}
