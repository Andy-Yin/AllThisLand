using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存验收标准
    /// </summary>
    public class ReqSaveTaskCheckStandard : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中添加 ＞0为项目中添加
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 验收标准id：0为新增
        /// </summary>
        public int StandardId { get; set; }

        /// <summary>
        /// 标准名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标准内容
        /// </summary>
        public string Desc { get; set; }
    }
}
