using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    [Table("T_AuditLog")]
    public partial class T_AuditLog
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// userid
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 主机IP
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 方法名
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 请求的方式(1:GET;2:POST;3:PUT;4:DELETE;5:HEAD;6:OPTIONS;7:TRACE;8:CONNECT;)
        /// </summary>
        public int Method { get; set; }

        /// <summary>
        /// 来源(0:无来源;1:机构;2:APP;3:OBS;4:平台;5:MessageApi;6:接口合并之后的拦截)
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// 执行时间(毫秒ms)
        /// </summary>
        public double ExecutionTime { get; set; }

        /// <summary>
        /// 是否是测试环境
        /// </summary>
        public bool IsDebug { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 请求参数-json字符串
        /// </summary>
        public string RequestParameter { get; set; }

        /// <summary>
        /// 返回参数-json字符串
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}