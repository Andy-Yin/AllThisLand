using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace Lhs.Interface
{
    public interface IConstructionTemplateRepository : IPlatformBaseService<T_ConstructionManageTemplate>
    {
        /// <summary>
        /// 获取模板
        /// </summary>
        /// <param name="type">类型：1 主材 2 地采 3 施工管理 4 施工计划 5 辅料 6 质检</param>
        /// <param name="name">搜索条件：名称</param>
        Task<List<TemplateInfo>> GetTemplateList(int type, string name);
    }
}
