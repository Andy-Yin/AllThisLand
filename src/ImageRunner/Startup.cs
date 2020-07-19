using Autofac;
using Core.Util;
using Core.Util.SettingModel;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lhs.Common;
using Lhs.Interface;
using System;
using System.IO;
using System.Reflection;
using Lhs.Service;
using Microsoft.Extensions.FileProviders;

namespace ImageRunner
{
    /// <summary>
    /// 启动设置
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 日志仓储
        /// </summary>
        public static ILoggerRepository repository { get; set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置</param>
        /// <param name="env">环境参数</param>
        public Startup(IConfiguration configuration)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
                //.AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            // 配置日志
            repository = LogManager.CreateRepository("PlatformApiRepository");
            // 指定日志配置文件 并且监听文件的变化
            XmlConfigurator.ConfigureAndWatch(repository, new FileInfo("log4net.config"));
            // 配置初始化
            Configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHeroRepository, HeroRepository>();
        }
    }
}
