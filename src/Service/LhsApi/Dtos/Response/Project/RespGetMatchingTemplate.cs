using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Core.Util;
using Google.Protobuf.WellKnownTypes;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Entity.ForeignDtos.Response.Project;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Response.Project
{


    public class RespGetMatchingTemplate
    {
        public RespGetMatchingTemplate() { }

        public RespGetMatchingTemplate(MatchingTemplate match)
        {
            Type = match.Type;
            TemplateId = match.TemplateId;
            TemplateName = match.TemplateName;
        }

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

        /// <summary>
        /// 是否可编辑 todo：不可编辑的限制条件待确认，先默认都可编辑
        /// </summary>
        public bool IsEditable { get; set; } = true;
    }

}
