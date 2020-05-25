using System;

namespace Lhs.Entity.ForeignDtos.Request.Project
{
    /// <summary>
    /// 关联的模板
    /// </summary>
    public class MatchingTemplate
    {
        /// <summary>
        /// 类型：1 预交底 2 交底 3 施工管理 4 施工计划
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName { get; set; }

    }
}
