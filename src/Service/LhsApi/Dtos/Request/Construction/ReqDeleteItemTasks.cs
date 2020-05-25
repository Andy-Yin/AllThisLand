using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 删除任务
    /// </summary>
    public class ReqDeleteItemTasks : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中操作 ＞0为项目中操作
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 类目id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }
    }
}
