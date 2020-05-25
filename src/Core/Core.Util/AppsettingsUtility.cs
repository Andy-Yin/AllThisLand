using Core.Util.SettingModel;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Core.Util
{
    /// <summary>
    /// 全局获取app的设置工具类
    /// </summary>
    public class AppsettingsUtility
    {
        /// <summary>
        /// log4net
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// serviceProvider
        /// </summary>
        private static ServiceProvider serviceProvider;

        /// <summary>
        /// _services
        /// </summary>
        private static IServiceCollection _services;

        /// <summary>
        /// _configuration
        /// </summary>
        private static IConfiguration _configuration;

        /// <summary>
        /// 初始化工具类
        /// </summary>
        /// <param name="provider"></param>
        public AppsettingsUtility(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
            // 仓库名 统一的
            log = LogManager.GetLogger("PlatformApiRepository", typeof(AppsettingsUtility));
            if (_services == null || _configuration == null)
            {
                log.Error("初始化配置工具类发生异常:_services或_configuration为空");
                throw new NullReferenceException("初始化配置工具类发生异常");
            }
            try
            {
                serviceProvider = _services.BuildServiceProvider();
            }
            catch (Exception ex)
            {
                log.Error("_services.BuildServiceProvider()失败:" + ex.ToString());
            }
        }

        /// <summary>
        /// 获取IOptionsMonitor<T>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>IOptionsMonitor<T></returns>
        public static IOptionsMonitor<T> GetMonitor<T>()
        {
            if (serviceProvider == null)
            {
                throw new NullReferenceException("获取失败,ServiceProvider为空");
            }
            if (typeof(T) != typeof(AllSetting))
            {
                // TODO: 要限定传递的参数值获取每个setting都注册一遍
            }
            return serviceProvider.GetRequiredService<IOptionsMonitor<T>>();
        }

        /// <summary>
        /// 获取单个的设置实体
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>T</returns>
        public static T GetSettingsModel<T>()
        {
            if (serviceProvider == null)
            {
                throw new NullReferenceException("获取失败,ServiceProvider为空");
            }
            if (typeof(T) != typeof(AllSetting))
            {
                // TODO: 要限定传递的参数值获取每个setting都注册一遍
            }
            return serviceProvider.GetRequiredService<IOptionsMonitor<T>>().CurrentValue;
        }

        /// <summary>
        /// 通过key获取设置并转换为T
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="key">key</param>
        /// <returns>设置内容</returns>
        public static string GetSetting(string key)
        {
            if (_configuration == null)
            {
                throw new NullReferenceException("获取失败,IConfiguration为空");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new NullReferenceException("获取失败,key不能为空");
            }
            return _configuration.GetSection(key).Value;
        }
    }
}
