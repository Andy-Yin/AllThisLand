using Microsoft.Extensions.DependencyInjection;
using Lhs.Interface;
using System;
using System.Linq;
using System.Reflection;

namespace LhsAPI
{
    /// <summary>
    /// 自动批量DI的数据拓展
    /// </summary>
    [Obsolete("此方法已弃用，在netcore3.0中使用了新的方法，用autoFac注入",true)]
    public static class DataServiceExtension
    {
        /// <summary>
        /// 从指定程序集中找出所有的仓储实现，注册到 DI 容器中
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, Assembly[] assembly)
        {
            Type types = typeof(IDenpendencyRepository);
            var repositoryTypes = assembly.SelectMany(
                ass => ass.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && types.IsAssignableFrom(t) &&
                t.GetInterfaces().Contains(types))).ToList();

            foreach (var repositoryType in repositoryTypes)
            {
                var implementedInterfaces = repositoryType.GetInterfaces().Where(a => a != typeof(IDisposable) && a != types);
                foreach (Type implementedInterface in implementedInterfaces)
                {
                    services.AddSingleton(implementedInterface, repositoryType);
                }
            }

            return services;
        }

        /// <summary>
        /// 从指定程序集中找出所有的应用层服务实现，注册到 DI 容器中
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAppServices(this IServiceCollection services, Assembly assembly)
        {
            return services;
        }
    }

}
