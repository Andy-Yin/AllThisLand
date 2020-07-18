using Lhs.Entity.DbEntity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data;

namespace Lhs.Interface
{
    /// <summary>
    /// 审计日志
    /// </summary>
    public interface IAuditRepository : IPlatformBaseService<T_AuditLog>
    {

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
        void SaveSystemMonitor(int userId, string hostName, string actionName, string controllerName,
            string methodStr, string source, string ip, double executeTime, string requestParam, string responseContent, bool dev);

    }
}
