using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Interface
{
    public interface ILocalMaterialItemRepository : IPlatformBaseService<T_LocalMaterialItem>
    {
        /// <summary>
        /// 删除基础数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_LocalMaterialItem>> GetList();
    }
}
