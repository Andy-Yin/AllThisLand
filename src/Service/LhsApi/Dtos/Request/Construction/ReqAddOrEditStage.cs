using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存阶段
    /// </summary>
    public class ReqAddOrEditStage : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中添加 ＞0为项目中添加
        /// </summary>
        public int ProjectId { get; set; }

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
