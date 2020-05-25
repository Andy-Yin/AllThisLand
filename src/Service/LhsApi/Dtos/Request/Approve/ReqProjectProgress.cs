using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Approve
{
    public class ReqProjectProgress : ReqAuth
    {
        /// <summary>
        /// 类型：1 发包确认 2 预交底审批 3 订单采购审批 4 交底验收审批
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型：1 工长 2 家装设计师 3 家装部长 4 监理
        /// 5 家居设计师 6 家居部长 7 工程助理 8 工程部长 9 客户 
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 提交类型： 1 提交 2 同意 3驳回
        /// </summary>
        public int Approved { get; set; }

        /// <summary>
        /// 、驳回原因
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int ApproveUserId { get; set; }

    }
}
