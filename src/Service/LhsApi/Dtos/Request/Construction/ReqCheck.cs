using System.Collections.Generic;
using Core.Util;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 验收
    /// </summary>
    public class ReqCheck : ReqAuth
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 验收任务id
        /// </summary>
        public int CheckId { get; set; }

        /// <summary>
        /// 验收人id
        /// </summary>
        public string ApproveUserId { get; set; }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool Approve { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 地理位置
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public List<string> Images { get; set; } = new List<string>();
    }
}
