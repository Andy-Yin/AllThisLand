using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 表T_ConstructionManageTemplateItem的Service
    /// </summary>
    public class ConstructionManageTemplateItemRepository : PlatformBaseService<T_ConstructionManageTemplateItem>, IConstructionManageTemplateItemRepository
    {
        public ConstructionManageTemplateItemRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取模板下的数据
        /// </summary>
        public async Task<List<T_ConstructionManageTemplateItem>> GetTemplateItemList(int templateId)
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ConstructionManageTemplateItem WHERE IsDel=0 AND TemplateId=@templateId";
                return (await coon.QueryAsync<T_ConstructionManageTemplateItem>(sql, new { templateId })).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ConstructionManageTemplateItem SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
