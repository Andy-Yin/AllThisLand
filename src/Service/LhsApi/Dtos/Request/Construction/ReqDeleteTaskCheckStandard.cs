using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 删除验收标准
    /// </summary>
    public class ReqDeleteTaskCheckStandard : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中操作 ＞0为项目中操作
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 验收标准id
        /// </summary>
        public List<int> StandardIds { get; set; }
    }
}
