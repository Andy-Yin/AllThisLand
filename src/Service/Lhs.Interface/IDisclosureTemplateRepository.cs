using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace Lhs.Interface
{
    public interface IDisclosureTemplateRepository : IPlatformBaseService<T_DisclosureTemplate>
    {
        /// <summary>
        /// 获取预交底、交底模板
        /// </summary>
        /// <param name="type">0 预交底 1 交底</param>
        /// <param name="name">搜索条件：名称</param>
        Task<List<T_DisclosureTemplate>> GetTemplateList(int type, string name);

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：1 预交底 2交底</param>
        Task<bool> DeleteTemplate(List<int> ids, int type);

        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="type">类型：0 预交底 1交底</param>
        /// <param name="templateId">模板id</param>
        Task<List<DisclosureItemInfo>> GetTemplateItemList(int type, int templateId);

        /// <summary>
        /// 获取项目的交底内容
        /// </summary>
        /// <param name="type">类型：0 预交底 1交底</param>
        /// <param name="projectId">项目id</param>
        Task<List<DisclosureItemInfo>> GetProjectItemList(int type, int projectId);
    }
}
