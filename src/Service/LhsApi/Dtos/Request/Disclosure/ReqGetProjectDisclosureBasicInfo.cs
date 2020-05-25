using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Disclosure
{
    /// <summary>
    /// 获取模板内容
    /// </summary>
    public class ReqGetProjectDisclosureBasicInfo : ReqAuth
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 基础项名称
        /// </summary>
        public string PreDisclosure { get; set; }

        /// <summary>
        /// 模板id 
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 类型：1 预交底 2交底验收
        /// </summary>
        public int Type { get; set; }

    }
}
