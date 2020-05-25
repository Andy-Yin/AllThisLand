using AspectCore.Extensions.DependencyInjection;
using Autofac;
using Core.Util;
using Core.Util.SettingModel;
using EasyCaching.Interceptor.AspectCore;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Lhs.Common;
using Lhs.Interface;
using LhsAPI.Authorization.Jwt;
using LhsAPI.Middleware;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using WebApiClient;

namespace LhsAPI
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
        /// 环境
        /// </summary>
        protected IWebHostEnvironment HostingEnvironment { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置</param>
        /// <param name="env">环境参数</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            HostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

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
            services.AddControllersWithViews(options =>
            {
                // 全局注入，所有的都会被拦截
                // 不用添加特性头 [ServiceFilter(typeof(xxx))]
                // 添加错误拦截
                 options.Filters.Add<ApiErrorsAttributeFilter>();
            }).ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        // 设置返回400的badRequest错误的格式
                        var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ResponseMessage
                        {
                            ErrMsg = string.Join(",", e.Value.Errors.Select(m => m.ErrorMessage)),
                            Data = "",
                            ErrCode = MessageResultCode.BadRequest
                        }).FirstOrDefault();
                        return new BadRequestObjectResult(errors);
                    };
                }) // 必须添加的
              .AddControllersAsServices();

            // 注入设置类到管道中
            services.AddOptions();
            services.Configure<AllSetting>(Configuration);

            // 初始化工具类
            new AppsettingsUtility(services, Configuration);

            // 配置Swagger
            services.AddSwaggerGen(options =>
            {
                //为Swagger的JSON和UI设置XML注释
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "LhsApi.xml");
                options.IncludeXmlComments(xmlPath);

                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // 很重要！这里配置安全校验，和之前的版本不一样
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//Jwt default param name
                    In = ParameterLocation.Header,//Jwt store address
                    Type = SecuritySchemeType.ApiKey//Security scheme type
                });
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = " API 文档",
                    Description = "by Andy Yin"
                });
            });

            // 配置redis,https://github.com/imperugo/StackExchange.Redis.Extensions/blob/master/README.md
            // 用Easy Cache替代Redis https://github.com/dotnetcore/EasyCaching
            services.AddEasyCaching(options =>
            {
                options.UseRedis(Configuration);
            });

            services.ConfigureAspectCoreInterceptor(options =>
            {
                // 可以在这里指定你要用那个provider
                // 或者在Attribute上面指定
                options.CacheProviderName = "redis1";
            });

            #region Configure Jwt Authentication

            var issuer = Configuration["Jwt:Issuer"];
            var audience = Configuration["Jwt:Audience"];
            var expire = Configuration["Jwt:ExpireMinutes"];
            var expiration = TimeSpan.FromMinutes(Convert.ToDouble(expire));
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"]));
            services.AddAuthentication(s =>
            {
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(s =>
            {
                s.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = key,
                    ClockSkew = expiration,
                    ValidateLifetime = true
                };
                s.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        //Token expired
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            #endregion

            // 配置AspectCore动态代理，
            // https://github.com/dotnetcore/AspectCore-Framework/blob/master/docs/1.%E4%BD%BF%E7%94%A8%E6%8C%87%E5%8D%97.md
            //services.ConfigureDynamicProxy(config =>
            //{
            //    //全局日志拦截--在service上，拦截所有“Platform.Interface”命名空间的方法
            //    //config.Interceptors.AddTransient<LogInterceptorAttribute>(Predicates.ForNameSpace("PlatformAPI.Controllers"));
            //    //缓存拦截：https://github.com/dotnetcore/EasyCaching/issues/53
            //});
            services.ConfigureDynamicProxy();
        }

        /// <summary>
        /// autoFac使用了UseServiceProviderFactory
        /// 必须写这个方法，不然不走
        /// 在ConfigureServices方法之后调用
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //添加任何Autofac模块或注册。
            //这是在ConfigureServices之后调用的，所以
            //在此处注册将覆盖在ConfigureServices中注册的内容。
            //在构建主机时必须调用“UseServiceProviderFactory（new AutofacServiceProviderFactory（））”`否则将不会调用此方法
            //var basePath = ApplicationEnvironment.ApplicationBasePath;
            try
            {
                //Service是继承接口的实现方法类库名称
                var assemblys = Assembly.Load("Lhs.Service");
                //IDependency 是一个接口（所有要实现依赖注入的借口都要继承该接口）
                var baseType = typeof(IDenpendencyRepository);
                builder.RegisterAssemblyTypes(assemblys)
                    .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                    .AsImplementedInterfaces().InstancePerLifetimeScope();
                // 这俩需要单独注册
                builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
                builder.RegisterType<JwtAppService>().As<IJwtAppService>();
            }
            catch (Exception e)
            {
                Console.WriteLine("注入失败：" + e);
            }
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"upload")),
                RequestPath = new PathString("/upload")
            });

            app.UseCalculateExecutionTime();
            app.UseCors(cfg =>
            {
                cfg.AllowAnyOrigin(); //对应跨域请求的地址
                cfg.AllowAnyMethod(); //对应请求方法的Method
                cfg.AllowAnyHeader(); //对应请求方法的Headers
            });
            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });

            //app.UseCalculateExecutionTime();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
            // 注册类库读取APP setting
            UtilServiceProvider.ServiceProvider = app.ApplicationServices;
        }
    }
}
