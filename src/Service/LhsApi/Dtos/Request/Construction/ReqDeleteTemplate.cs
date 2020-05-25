using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 删除模板
    /// </summary>
    public class ReqDeleteTemplate : ReqAuth
    {
        /// <summary>
        /// 要删除的id
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// 类型：3 施工管理 4 施工计划
        /// </summary>
        public int Type { get; set; }

    }
}
