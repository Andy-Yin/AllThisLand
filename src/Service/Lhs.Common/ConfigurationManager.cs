using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Lhs.Common
{
    /// <summary>
    /// 读取APPSetting帮助类
    /// </summary>
    public static class ConfigurationHelper
    {
        public static IConfiguration AppSetting { get; private set; }
        static ConfigurationHelper()
        {
            try
            {
                Microsoft.AspNetCore.Hosting.IHostingEnvironment env = UtilServiceProvider.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
                AppSetting = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                    //.AddEnvironmentVariables()
                    .Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
    public class UtilServiceProvider
    {
        public static IServiceProvider ServiceProvider { get; set; }
    }
}
