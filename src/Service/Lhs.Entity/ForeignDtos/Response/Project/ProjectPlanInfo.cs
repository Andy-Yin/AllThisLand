using System;
using System.Collections.Generic;
using Core.Util;
using Lhs.Common;
using Lhs.Common.Enum;

namespace Lhs.Entity.ForeignDtos.Response.Project
{
    public class ProjectPlanInfo
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ProjectEnum.ProjectPlanType Type { get; set; }

        private string _planName;

        /// <summary>
        /// 计划名称
        /// </summary>
        public string PlanName
        {
            get
            {
                switch (Type)
                {
                    case ProjectEnum.ProjectPlanType.PackageApprove:
                    case ProjectEnum.ProjectPlanType.PreDisclosureApprove:
                    case ProjectEnum.ProjectPlanType.OrderApprove:
                    case ProjectEnum.ProjectPlanType.DisclosureApprove:
                        return EnumHelper.GetDescription(typeof(ProjectEnum.ProjectPlanType), Type);
                    case ProjectEnum.ProjectPlanType.MeasureTask:
                        return $"{_planName}测量";
                    case ProjectEnum.ProjectPlanType.OrderTask:
                        return $"{_planName}下单";
                    case ProjectEnum.ProjectPlanType.InstallTask:
                        return $"{_planName}安装";
                }
                return _planName;
            }
            set => _planName = value;
        }

        /// <summary>
        /// 实际开工时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 实际完工时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        public string ActualTime
        {
            get
            {
                var time = string.Empty;
                if (!string.IsNullOrEmpty(StartTime?.ToString(CommonMessage.DateFormatYMDHM)))
                {
                    time += $"{StartTime?.ToString(CommonMessage.DateFormatYMDHM)}~";
                    if (!string.IsNullOrEmpty(EndTime?.ToString(CommonMessage.DateFormatYMDHM)))
                    {
                        time += EndTime?.ToString(CommonMessage.DateFormatYMDHM);
                    }
                }
                return time;
            }
        }

        /// <summary>
        /// 计划状态
        /// </summary>
        public ProjectEnum.ProjectPlanStatus Status
        {
            get
            {
                if (StartTime != null)
                {
                    if (EndTime != null)
                    {
                        return ProjectEnum.ProjectPlanStatus.Finished;
                    }
                    return ProjectEnum.ProjectPlanStatus.Started;
                }
                return ProjectEnum.ProjectPlanStatus.WaitStart;
            }
        }
    }
}
