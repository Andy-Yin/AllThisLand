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
    /// 阶段列表
    /// </summary>
    public class RespStageList
    {
        public RespStageList()
        {
        }

        public RespStageList(T_ConstructionPlanStage stage)
        {
            StageId = stage.Id;
            StageName = stage.Name;
            Cycle = stage.Days;
        }

        public RespStageList(T_ProjectPlanStage stage)
        {
            StageId = stage.Id;
            StageName = stage.Name;
            Cycle = stage.Days;
        }

        /// <summary>
        /// 阶段id
        /// </summary>
        public int StageId { get; set; }

        /// <summary>
        /// 阶段名称
        /// </summary>
        public string StageName { get; set; }

        /// <summary>
        /// 周期（天数）
        /// </summary>
        public int Cycle { get; set; }

    }
}
