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
    /// 人工费
    /// </summary>
    public class ProjectMaterialLabourRepository : PlatformBaseService<T_ProjectMaterialLabour>, IProjectMaterialLabourRepository
    {
        public ProjectMaterialLabourRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectMaterialLabour>> GetListByProjectId(int projectId)
        {
            using (var coon = Connection)
            {
                var sql = $"SELECT * FROM dbo.T_ProjectMaterialLabour WHERE IsDel=0 and projectid = {projectId}";

                var list = await coon.QueryAsync<T_ProjectMaterialLabour>(sql);
                if (!list.Any())
                {
                    return new List<T_ProjectMaterialLabour>();
                }

                return list.ToList();
            }
        }
    }
}
