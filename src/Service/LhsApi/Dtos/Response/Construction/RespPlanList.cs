using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using NPOI.SS.UserModel;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 施工计划基础数据列表
    /// </summary>
    public class RespPlanList
    {
        public RespPlanList()
        {
        }

        public RespPlanList(T_ConstructionPlanItem plan, List<T_ConstructionPlanTemplateItem> items, List<T_ConstructionPlanStage> stages)
        {
            ItemId = plan.Id;
            ItemName = plan.Name;
            Content = string.IsNullOrEmpty(plan.Contents) ? new List<string>() : plan.Contents.Split(CommonConst.Separator).ToList();
            InternalControlCycle = $"{plan.InnerDays}天";
            ContractCycle = $"{plan.ContractDays}天";
            InternalControlDays = plan.InnerDays;
            ContractCycleDays = plan.ContractDays;
            StageId = plan.StageId;
            if (plan.StageId > 0 && stages.Any(n => n.Id == plan.StageId))
            {
                var stageInfo = stages.FirstOrDefault(n => n.Id == plan.StageId);
                if (stageInfo != null)
                {
                    ContractCycle = $"{stageInfo.Name}{stageInfo.Days}天";
                }
            }
            Selected = items.Any(n => n.CategoryId == plan.Id);
        }

        public RespPlanList(T_ConstructionPlanItem plan, List<T_ConstructionPlanStage> stages)
        {
            ItemId = plan.Id;
            ItemName = plan.Name;
            Content = string.IsNullOrEmpty(plan.Contents) ? new List<string>() : plan.Contents.Split(CommonConst.Separator).ToList();
            InternalControlCycle = $"{plan.InnerDays}天";
            ContractCycle = $"{plan.ContractDays}天";
            InternalControlDays = plan.InnerDays;
            ContractCycleDays = plan.ContractDays;
            StageId = plan.StageId;
            if (plan.StageId > 0 && stages.Any(n => n.Id == plan.StageId))
            {
                var stageInfo = stages.FirstOrDefault(n => n.Id == plan.StageId);
                if (stageInfo != null)
                {
                    ContractCycle = $"{stageInfo.Name}{stageInfo.Days}天";
                }
            }
            Selected = false;
        }

        public RespPlanList(T_ProjectConstructionPlan plan, List<T_ProjectPlanStage> stages)
        {
            ItemId = plan.Id;
            ItemName = plan.Name;
            Content = string.IsNullOrEmpty(plan.Contents) ? new List<string>() : plan.Contents.Split(CommonConst.Separator).ToList();
            InternalControlCycle = $"{plan.Days}天";
            ContractCycle = $"{plan.ContractDays}天";
            InternalControlDays = plan.Days;
            ContractCycleDays = plan.ContractDays;
            StageId = plan.ProjectStageId;
            if (plan.ProjectStageId > 0 && stages.Any(n => n.Id == plan.ProjectStageId))
            {
                var stageInfo = stages.FirstOrDefault(n => n.Id == plan.ProjectStageId);
                if (stageInfo != null)
                {
                    ContractCycle = $"{stageInfo.Name}{stageInfo.Days}天";
                }
            }
            Selected = true;
        }

        /// <summary>
        /// id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public List<string> Content { get; set; }

        /// <summary>
        /// 周期（内控），例如10天
        /// </summary>
        public string InternalControlCycle { get; set; }

        /// <summary>
        /// 周期（内控）天数
        /// </summary>
        public int InternalControlDays { get; set; }

        /// <summary>
        /// 阶段id
        /// </summary>
        public int StageId { get; set; }

        /// <summary>
        /// 周期（合同）。例如阶段a(10天)
        /// </summary>
        public string ContractCycle { get; set; }

        /// <summary>
        /// 周期（合同）天数
        /// </summary>
        public int ContractCycleDays { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; } = false;

    }
}
