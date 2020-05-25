using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Disclosure
{
    /// <summary>
    /// 保存交底
    /// </summary>
    public class ReqSaveDisclosure : ReqAuth
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 类型：1 预交底 2交底验收
        /// </summary>
        public int Type { get; set; }

    }
}
