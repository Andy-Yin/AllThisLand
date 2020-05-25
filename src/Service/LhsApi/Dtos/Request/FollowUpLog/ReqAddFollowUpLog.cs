using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.FollowUpLog
{
    public class ReqAddFollowUpLog : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 提交的用户的Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 跟进日志分类
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 附件图片,多个用:|隔开
        /// </summary>
        public string Images { get; set; }
    }
}
