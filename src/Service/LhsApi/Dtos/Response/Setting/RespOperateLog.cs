using System.Collections.Generic;
using System.Linq;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;
using LhsApi.Dtos.Response.Setting;

namespace LhsAPI.Dtos.Response.Setting
{
    public class RespOperateLog
    {
        public RespOperateLog()
        {
        }

        public RespOperateLog(OperateLog log)
        {
            Time = log.CreateTime.ToString(CommonMessage.DateFormatYMDHMS);
            Ip = log.Ip ?? string.Empty;
            UserName = log.Name ?? string.Empty;
            Source = log.Source;
            Content = $"{log.ActionName}{log.RequestParameter}";
        }

        /// <summary>
        /// 
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
