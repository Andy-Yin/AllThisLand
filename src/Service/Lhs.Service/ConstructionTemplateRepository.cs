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
using Core.Util;
using Dapper.Contrib.Extensions;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace Lhs.Service
{
    /// <summary>
    /// 施工管理
    /// </summary>
    public class ConstructionTemplateRepository : PlatformBaseService<T_ConstructionManageTemplate>, IConstructionTemplateRepository
    {
        public ConstructionTemplateRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="type">类型：1 主材 2 地采 3 施工管理 4 施工计划 5 辅料 6 质检</param>
        /// <param name="name">搜索条件：名称</param>
        public async Task<List<TemplateInfo>> GetTemplateList(int type, string name)
        {
            using (var coon = Connection)
            {
                var tableName = EnumHelper.GetDescription(typeof(ConstructionEnum.ConstructionTemplateType), type);
                string sql;
                if (string.IsNullOrEmpty(name))
                {
                    sql = $"SELECT * FROM dbo.{tableName} WITH (NOLOCK) WHERE IsDel=0";
                    return (await coon.QueryAsync<TemplateInfo>(sql)).ToList();
                }
                sql = $"SELECT * FROM dbo.{tableName} WITH (NOLOCK) WHERE IsDel=0 AND Name LIKE @name";
                return (await coon.QueryAsync<TemplateInfo>(sql, new { name = $"%{name}%" })).ToList();
            }
        }

    }
}
