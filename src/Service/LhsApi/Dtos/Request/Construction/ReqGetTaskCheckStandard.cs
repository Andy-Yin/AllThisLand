using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 任务下的验收标准
    /// </summary>
    public class ReqGetTaskCheckStandard : ReqAuth
    {
        /// <summary>
        /// 项目id：＞0为项目的验收标准 =0为基础数据中的验收标准
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 搜索条件：名称
        /// </summary>
        public string SearchName { get; set; }
    }
}
