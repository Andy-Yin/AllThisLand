using System;
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
    /// 物料管理日志
    /// </summary>
    public class ProjectMaterialItemLogRepository : PlatformBaseService<T_ProjectMaterialItemLog>, IProjectMaterialItemLogRepository
    {
        public ProjectMaterialItemLogRepository(IConfiguration config)
        {
            Config = config;
        }


        public async Task<List<T_ProjectMaterialItemLog>> GetLogListByItemId(int itemId)
        {
            string sql = @$" SELECT * FROM T_ProjectMaterialItemLog WHERE ProjectMaterialItemId = {itemId}";
            var list = (await this.FindAllAsync<T_ProjectMaterialItemLog>(sql)).ToList();

            return list;
        }
    }
}
