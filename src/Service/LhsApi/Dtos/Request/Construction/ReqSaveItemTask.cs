using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存任务
    /// </summary>
    public class ReqSaveItemTask : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中添加 ＞0为项目中添加
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 所属类目
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 任务id：0为新增
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
    }
}
