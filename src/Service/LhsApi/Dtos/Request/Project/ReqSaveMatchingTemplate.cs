using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Project
{
    /// <summary>
    /// 提交项目匹配的模板
    /// </summary>
    public class ReqSaveMatchingTemplate : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中添加 ＞0为项目中添加
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 类型：1预交底 2交底验收 3施工管理 4施工计划 5质检管理
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public int TemplateId { get; set; }
    }
}
