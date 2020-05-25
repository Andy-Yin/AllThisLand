using System;
using System.Data;
using System.Data.SqlClient;

namespace Core.Data.SQLServer
{
    public class SQLServerDbConnection
    {
        [ThreadStatic]
        private static IDbConnection _db = null;

        /// <summary>
        /// 根据传入的字符串链接地址创建sqlserver访问对象
        /// </summary>
        /// <param name="connString">数据库链接字符串</param>
        public static IDbConnection GetSqlServerDbConnection(string connString)
        {
            if (string.IsNullOrEmpty(connString))
                connString = "";//Globals.Configuration["Db:ConnString"];
            _db = new SqlConnection(connString);
            return _db;
        }

        /// <summary>
        /// 默认的数据库配置文件读取配置
        /// 获取sqlserver链接
        /// </summary>
        public static IDbConnection GetSqlServerDbConnection()
        {
            if (_db != null)
                return _db;
            var connString = "111";// Globals.Configuration["Db:ConnString"];
            _db = new SqlConnection(connString);
            return _db;
        }
    }
}
