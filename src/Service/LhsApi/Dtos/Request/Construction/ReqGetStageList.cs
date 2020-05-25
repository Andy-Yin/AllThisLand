using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 获取施工计划的阶段列表
    /// </summary>
    public class ReqGetStageList : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中操作 ＞0为项目中操作
        /// </summary>
        public int ProjectId { get; set; }
    }
}
