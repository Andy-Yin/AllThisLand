using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IBaseService<T> where T : class
    {
        Task<bool> IsExist<T>(int id);

        Task<int> Count(string sql);

        Task<U> ExecuteScalarAsync<U>(string sql, object param = null);

        Task<int> ExecuteAsync(string sql, object param = null);

        Task<int> AddAsync(T t);

        Task<int> AddAsync(SqlConnection dbConnection, T t, IDbTransaction transaction);

        Task<int> AddListAsync(List<T> t);

        Task<int> AddListAsync(SqlConnection dbConnection, List<T> t, IDbTransaction transaction);

        Task<bool> UpdateAsync(T t);

        Task<bool> UpdateAsync(SqlConnection dbConnection, T t, IDbTransaction transaction);

        Task<bool> UpdateListAsync(List<T> t);

        Task<bool> UpdateListAsync(SqlConnection dbConnection, List<T> t, IDbTransaction transaction);

        Task<T> SingleAsync(int id);

        Task<IEnumerable<T>> AllAsync();

        Task<IEnumerable<T>> AllAsync(string @sql, object param = null);

        /// <summary>
        /// 不建议使用该方法
        /// 根据SQL语句获取符合条件的所有对象
        /// </summary>
        Task<IEnumerable<TU>> FindAllAsync<TU>(string @sql, object param = null);

        /// <summary>
        /// 不建议使用该方法
        /// 获取一个表的所有数据（慎用）性能问题
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        Task<IEnumerable<T>> FindAllAsync<T>() where T : class;

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="sql">完整的查询语句</param>
        /// <param name="param">过滤条件</param>
        /// <param name="sortBy">排序</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <param name="pageSize">每页的个数</param>
        Task<PageResponse<T>> PagedAsync<T>(string sql, object param, string sortBy, long pageIndex, long pageSize);
    }
}
