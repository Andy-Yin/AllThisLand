using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    /// <summary>
    /// 框架的service基类，不要直接引用Base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public IConfiguration Config;

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(Config.GetConnectionString("DefaultConnection"));
            }
        }

        /// <summary>
        /// 在自己的service里单独实现，基类里需要判断表名，或者isDel等字段，所以不放到本文件里边。
        /// 参考：var exists = conn.ExecuteScalar<bool>("select count(1) from Table where Id=@id", new {id});
        /// </summary>
        public async Task<bool> IsExist<T>(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 在自己的service里单独实现，基类里需要判断表名，或者isDel等字段，所以不放到本文件里边。
        /// 参考：var exists = conn.ExecuteScalar<bool>("select count(1) from Table where Id=@id", new {id});
        /// </summary>
        public async Task<int> Count(string sql)
        {
            throw new NotImplementedException();
        }

        public async Task<TU> ExecuteScalarAsync<TU>(string sql, object param = null)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                return await conn.ExecuteScalarAsync<TU>(sql, param);
            }
        }

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.ExecuteAsync(sql, param);
            }
        }

        public async Task<int> AddAsync(T t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.InsertAsync<T>(t);
            }
        }

        /// <summary>
        /// 带事务的插入
        /// </summary>
        public async Task<int> AddAsync(SqlConnection dbConnection, T t, IDbTransaction transaction)
        {
            return await dbConnection.InsertAsync<T>(t, transaction);
        }

        public async Task<int> AddListAsync(List<T> t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.InsertAsync<List<T>>(t);
            }
        }

        public async Task<int> AddListAsync(SqlConnection dbConnection, List<T> t, IDbTransaction transaction)
        {
            return await dbConnection.InsertAsync<List<T>>(t, transaction);
        }

        /// <summary>
        /// 带事务的更新
        /// </summary>
        public async Task<bool> UpdateAsync(SqlConnection dbConnection, T t, IDbTransaction transaction)
        {
            return await dbConnection.UpdateAsync<T>(t, transaction);
        }

        public async Task<bool> UpdateAsync(T t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.UpdateAsync<T>(t);
            }
        }
        public async Task<bool> UpdateListAsync(List<T> t)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.UpdateAsync<List<T>>(t);
            }
        }

        public async Task<bool> UpdateListAsync(SqlConnection dbConnection, List<T> t, IDbTransaction transaction)
        {
            return await dbConnection.UpdateAsync<List<T>>(t, transaction);
        }

        public async Task<T> SingleAsync(int id)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.GetAsync<T>(id);
            }
        }

        public async Task<IEnumerable<T>> AllAsync()
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.GetAllAsync<T>();
            }
        }

        public async Task<IEnumerable<T>> AllAsync(string @sql, object param = null)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<T>(sql, param);
            }
        }

        /// <summary>
        /// 不建议使用这个方法
        /// 查找全部，用于类型在后面
        /// </summary>
        /// <typeparam name="TU">要返回的类型</typeparam>
        /// <param name="sql">全部的sql，不只是一个where条件</param>
        /// <param name="param">参数</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<TU>> FindAllAsync<TU>(string @sql, object param = null)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<TU>(sql, param);
            }
        }

        /// <summary>
        /// 不建议使用这个方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync<T>() where T : class
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();
                return await dbConnection.GetAllAsync<T>();
            }
        }

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="sortBy">排序（必须字段）</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PageResponse<T>> PagedAsync<T>(string sql, object param, string sortBy, long pageIndex, long pageSize)
        {
            using (var dbConnection = Connection)
            {
                await dbConnection.OpenAsync();

                var pageResult = new PageResponse<T>();

                if (pageIndex < 1)
                    pageIndex = 1;
                pageResult.PageIndex = pageIndex;
                pageResult.PageSize = pageSize;

                var queryNumSql = new StringBuilder($" SELECT COUNT(1) FROM ({sql}) AS mapTable ");
                pageResult.TotalCount = dbConnection.ExecuteScalar<long>(queryNumSql.ToString(), param);
                if (pageResult.TotalCount == 0 || pageSize == 0)
                {
                    return pageResult;
                }

                var pageMinCount = (pageIndex - 1) * pageSize + 1;
                if (pageResult.TotalCount < pageMinCount)
                {
                    pageIndex = (int)((pageResult.TotalCount - 1) / pageSize) + 1;
                }

                pageResult.TotalPages = pageResult.TotalCount / pageSize;
                if ((pageResult.TotalCount % pageSize) != 0)
                    pageResult.TotalPages++;

                var querySql = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(sortBy))
                {
                    querySql.AppendLine(
                        $@" SELECT  mapTable.*
                            FROM    ( {sql}
                                    ) AS mapTable
                            ORDER BY {sortBy}
                                    OFFSET {(pageIndex - 1) * pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY");
                }
                else
                {
                    querySql.Append(
                        $@" SELECT  b.*
                            FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY ( SELECT NULL ) ) rn ,
                                                a.*
                                        FROM      ( {sql}
                                                ) a
                                    ) b
                            WHERE   b.rn > {(pageIndex - 1) * pageSize}
                                    AND b.rn <= {pageSize * pageIndex} ");
                }

                pageResult.Items = dbConnection.Query<T>(querySql.ToString(), param).ToList();
                return pageResult;

            }
        }
    }
}
