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
    public interface IDisclosureItemRepository : IPlatformBaseService<T_DisclosureItem>
    {
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <param name="type">0 预交底 1 交底</param>
        /// <param name="name">搜索条件：名称</param>
        Task<List<T_DisclosureItem>> GetDisclosureItemList(int type, string name);

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：1 预交底 2交底</param>
        Task<bool> DeleteDisclosureItem(List<int> ids, int type);

        /// <summary>
        /// 删除项目中的基础数据
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：1 预交底 2交底</param>
        /// <param name="projectId">项目id</param>
        Task<bool> DeleteProjectDisclosureItem(List<int> ids, int type, int projectId);

    }
}
