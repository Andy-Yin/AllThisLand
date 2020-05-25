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
    /// 表T_ConstructionPlanTemplateItem的Service
    /// </summary>
    public class ConstructionPlanTemplateItemRepository : PlatformBaseService<T_ConstructionPlanTemplateItem>, IConstructionPlanTemplateItemRepository
    {
        public ConstructionPlanTemplateItemRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取模板下的数据
        /// </summary>
        public async Task<List<T_ConstructionPlanTemplateItem>> GetTemplateItemList(int templateId)
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_ConstructionPlanTemplateItem WHERE IsDel=0 AND TemplateId=@templateId";
                return (await coon.QueryAsync<T_ConstructionPlanTemplateItem>(sql, new { templateId })).ToList();
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_ConstructionPlanTemplateItem SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
