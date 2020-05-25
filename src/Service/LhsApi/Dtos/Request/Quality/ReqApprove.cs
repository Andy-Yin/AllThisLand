using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqApprove : ReqAuth
    {
        /// <summary>
        /// 质检Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 审批类型1-工长，2-工程部长
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 提交用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 附件图片,多个用|隔开
        /// </summary>
        public string Images { get; set; }
    }
}
