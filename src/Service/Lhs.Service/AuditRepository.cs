using Core.Util;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Entity.DbEntity;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data;
using Lhs.Common.Const;
using LhsApi.Dtos.Request.Setting;
using LhsApi.Dtos.Response.Setting;

namespace Lhs.Service
{
    /// <summary>
    /// 审计日志
    /// </summary>
    public class AuditRepository : PlatformBaseService<T_AuditLog>, IAuditRepository
    {
        public AuditRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 存储监控信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="hostName">主机地址名称</param>
        /// <param name="actionName">方法名称</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="methodStr">请求方式</param>
        /// <param name="executeTime">执行时间</param>
        /// <param name="requestParam">请求参数</param>
        /// <param name="responseContent">返回参数</param>
        /// <param name="dev">是否是开发环境</param>
        public async void SaveSystemMonitor(int userId, string hostName, string actionName, string controllerName,
            string methodStr, string source, string ip, double executeTime, string requestParam, string responseContent, bool dev)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var sql = $@"
                INSERT INTO dbo.T_AuditLog
				(
					UserId,
					HostName,
					ActionName,
					ControllerName,
					Method,
					Source,
					Ip,
					ExecutionTime,
					IsDebug,
					CreateTime,
					RequestParameter,
					ResponseContent,
					Note
				)
				VALUES
				(   @userId,         -- UserId - int
					@hostName,        -- HostName - varchar(50)
					@actionName,        -- ActionName - varchar(100)
					@controllerName,        -- ControllerName - varchar(100)
					@method,         -- Method - tinyint
					@source,         -- Source - varchar(20)
					@ip,         -- Ip - varchar(20)
					@executeTime,      -- ExecutionTime - decimal(18, 2)
					@dev,      -- IsDebug - bit
					GETDATE(), -- CreateTime - datetime
					@requestParam,        -- RequestParameter - varchar(max)
					@responseContent,        -- ResponseContent - varchar(max)
					''         -- Note - varchar(max)
					)";
                var saveTask = await conn.ExecuteAsync(sql,
                    new
                    {
                        userId,
                        hostName,
                        actionName,
                        controllerName,
                        @method = SystemMonitorHelper.MethodStrConvertToByte(methodStr),
                        source,
                        ip,
                        executeTime,
                        dev,
                        requestParam,
                        responseContent
                    });
            }
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        public async Task<PageResponse<OperateLog>> GetOperateLogList(ReqDbOperateLog request)
        {
            var query = string.Empty;
            if (!string.IsNullOrEmpty(request.Source))
            {
                query += $" AND logs.Source = @Source";
            }
            if (!string.IsNullOrEmpty(request.UserName))
            {
                query += $" AND u.Name LIKE @UserName";
            }
            if (!string.IsNullOrEmpty(request.StartTime))
            {
                query += $" AND logs.CreateTime >= @StartTime";
            }
            if (!string.IsNullOrEmpty(request.EndTime))
            {
                query += $" AND logs.CreateTime <= @EndTime";
            }

            var sql = $@"
                SELECT logs.CreateTime,
                       logs.Ip,
                       u.Name,
                       logs.Source,
                       logs.ActionName,
                       logs.RequestParameter
                FROM dbo.T_AuditLog logs WITH (NOLOCK)
                    LEFT JOIN dbo.T_User u WITH (NOLOCK)
                        ON logs.UserId = u.Id
                WHERE logs.Method != 6
                    {query}";
            return await this.PagedAsync<OperateLog>(sql, new
            {
                request.Source,
                request.StartTime,
                request.EndTime,
                UserName = $"%{request.UserName}%"
            }, "CreateTime DESC", request.Page, CommonConst.PageSize);
        }
    }
}
