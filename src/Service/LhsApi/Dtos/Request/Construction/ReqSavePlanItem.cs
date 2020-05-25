using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存基础数据
    /// </summary>
    public class ReqSavePlanItem : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中添加 ＞0为项目中添加
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 基础数据id：0为新增
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 基础数据名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 周期（内控）天数
        /// </summary>
        public int InternalControlCycle { get; set; }

        /// <summary>
        /// 阶段id
        /// </summary>
        public int StageId { get; set; }

        /// <summary>
        /// 周期（合同）天数：StageId大于0 时，此字段不用
        /// </summary>
        public int ContractCycle { get; set; }
    }
}
