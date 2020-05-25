using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Disclosure
{
    /// <summary>
    /// 获取模板列表
    /// </summary>
    public class ReqDisclosureTemplate : ReqAuth
    {
        /// <summary>
        /// 类型：1 预交底 2交底验收
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 搜索条件：名称
        /// </summary>
        public string Name { get; set; }

    }
}
