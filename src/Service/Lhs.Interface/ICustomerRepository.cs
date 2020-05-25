using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_Customer的接口
    ///</summary>
    public interface ICustomerRepository : IPlatformBaseService<T_Customer>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        Task<List<T_Customer>> GetList();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        Task<T_Customer> GetCustomerInfo(string phone);

        /// <summary>
        /// 删除数据
        /// </summary>
        Task<bool> DeleteBasicItem(List<int> ids);
    }
}
