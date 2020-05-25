using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
using Enum = Google.Protobuf.WellKnownTypes.Enum;

namespace LhsAPI.Dtos.Response.Project
{
    /// <summary>
    /// 项目的施工计划
    /// </summary>
    public class RespPlanProgress
    {
        public RespPlanProgress() { }

        public RespPlanProgress(ProjectPlanInfo plan, T_Project project)
        {
            PlanName = plan.PlanName;
            PlanStartTime = $"{project.PlanStartDate.ToString(CommonMessage.DateFormatYMDHM)}~{project.PlanEndDate?.ToString(CommonMessage.DateFormatYMDHM)}";
            ActualStartTime = plan.ActualTime;
            Status = EnumHelper.GetDescription(typeof(ProjectEnum.ProjectPlanStatus), plan.Status);
        }

        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// 计划时间
        /// </summary>
        public string PlanStartTime { get; set; } = string.Empty;

        /// <summary>
        /// 实际时间
        /// </summary>
        public string ActualStartTime { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }
}
