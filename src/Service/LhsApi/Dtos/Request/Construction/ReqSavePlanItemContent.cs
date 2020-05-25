using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存基础数据
    /// </summary>
    public class ReqSavePlanItemContent : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中添加 ＞0为项目中添加
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 计划类目id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 内容列表
        /// </summary>
        public List<string> ContentName { get; set; }
    }
}
