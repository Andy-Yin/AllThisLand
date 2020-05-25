using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 获取模板列表
    /// </summary>
    public class ReqTemplate : ReqAuth
    {
        /// <summary>
        /// 类型：3 施工管理 4 施工计划
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 搜索条件：名称
        /// </summary>
        public string Name { get; set; }

    }
}
