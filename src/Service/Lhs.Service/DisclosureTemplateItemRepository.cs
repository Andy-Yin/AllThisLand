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
    /// 模板关联的基础数据
    /// </summary>
    public class DisclosureTemplateItemRepository : PlatformBaseService<T_DisclosureTemplateItem>, IDisclosureTemplateItemRepository
    {
        public DisclosureTemplateItemRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取模板下的基础数据id
        /// </summary>
        /// <param name="templateId">模板id</param>
        public async Task<List<T_DisclosureTemplateItem>> GetTemplateItemIds(int templateId)
        {
            using (var coon = Connection)
            {
                var sql =
                    @"SELECT * FROM dbo.T_DisclosureTemplateItem WITH(NOLOCK) WHERE TemplateId=@templateId AND IsDel=0";
                return (await coon.QueryAsync<T_DisclosureTemplateItem>(sql, new { templateId })).ToList();
            }
        }
    }
}
