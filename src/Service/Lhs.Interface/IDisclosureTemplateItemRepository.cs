using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    public interface IDisclosureTemplateItemRepository : IPlatformBaseService<T_DisclosureTemplateItem>
    {
        /// <summary>
        /// 获取模板下的基础数据id
        /// </summary>
        /// <param name="templateId">模板id</param>
        Task<List<T_DisclosureTemplateItem>> GetTemplateItemIds(int templateId);
    }
}
