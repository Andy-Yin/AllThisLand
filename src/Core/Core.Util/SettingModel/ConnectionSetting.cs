namespace Core.Util.SettingModel
{
    /// <summary>
    /// 数据库连接相关的类
    /// </summary>
    public class ConnectionSetting
    {
        /// <summary>
        /// 默认数据库连接
        /// </summary>
        public string DefaultConnection { get; set; }

        /// <summary>
        /// PgSql连接
        /// </summary>
        public string PgSqlConnection { get; set; }

        /// <summary>
        /// MySql连接
        /// </summary>
        public string MySqlConnection { get; set; }

        /// <summary>
        /// Oracle数据库连接
        /// </summary>
        public string OracleConnection { get; set; }
    }
}
