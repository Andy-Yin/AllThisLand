using System.Collections.Generic;

namespace Core.Util.SettingModel
{
    /// <summary>
    /// 缓存
    /// </summary>
    public class EasyCachingSetting
    {
        /// <summary>
        /// 缓存的Redis
        /// </summary>
        public EasyCacheRedisSetting Redis { get; set; }
    }

    /// <summary>
    /// EasyCacheRedisSetting 缓存
    /// </summary>
    public class EasyCacheRedisSetting
    {
        /// <summary>
        /// 最大读取时间
        /// </summary>
        public int MaxRdSecond { get; set; }

        /// <summary>
        /// 是否启用日志
        /// </summary>
        public bool EnableLogging { get; set; }

        /// <summary>
        /// 锁时间-毫秒
        /// </summary>
        public int LockMs { get; set; }

        /// <summary>
        /// 休眠时间-毫秒
        /// </summary>
        public int SleepMs { get; set; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        public EasyRedisDbSetting DbConfig { get; set; }
    }

    /// <summary>
    /// EasyCache
    /// </summary>
    public class EasyRedisDbSetting
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否是ssl
        /// </summary>
        public bool IsSsl { get; set; }

        /// <summary>
        /// ssl主机
        /// </summary>
        public string SslHost { get; set; }

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public int ConnectionTimeout { get; set; }

        /// <summary>
        /// 是否允许admin
        /// </summary>
        public bool AllowAdmin { get; set; }

        /// <summary>
        /// Redis数据库的端口暴露
        /// </summary>
        public List<EasyRedisDbPointSetting> EndPoints { get; set; }

        /// <summary>
        /// redis的数据库标号
        /// </summary>
        public int Database { get; set; }
    }

    /// <summary>
    ///  Redis数据库的端口暴露
    /// </summary>
    public class EasyRedisDbPointSetting
    {
        /// <summary>
        /// host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// port
        /// </summary>
        public int Port { get; set; }
    }
}
