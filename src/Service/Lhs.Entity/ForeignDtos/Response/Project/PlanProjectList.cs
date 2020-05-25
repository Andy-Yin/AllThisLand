using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.Project
{
    public class PlanProjectList
    {
        public string PlanName { get; set; } // 施工计划项目名称
        public string MaterialDetails { get; set; } // 物料匹配
        public string TimeLimit { get; set; } // 工期
        public string InternalControlCycle { get; set; }  // 周期（内控）
        public string ContractCycle { get; set; }  // 周期（合同）
        public string CalculationRule { get; set; } // 计算规则
        public string CalculationFromCheck { get; set; } // 交底验收起计算
        public string PlanStartTime { get; set; } // 计划开工时间
        public string RealStartTime { get; set; } // 实际开工时间
        public string Status { get; set; } // 状态
        public List<PlanDetails> PlanDetail { get; set; } = new List<PlanDetails>(); // 施工计划项目详细项
    }

    public class PlanDetails
    {
        public string PlanName { get; set; }
        public string MaterialDetails { get; set; } // 物料匹配
        public string TimeLimit { get; set; } // 工期
        public string PlanStartTime { get; set; }// 计划开工时间
        public string RealStartTime { get; set; } // 实际开工时间
        public string Status { get; set; } // 状态
    }
}
