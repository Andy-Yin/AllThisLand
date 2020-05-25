using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Disclosure
{
    /// <summary>
    /// 删除交底基础数据
    /// </summary>
    public class ReqDeleteDisclosureBasic : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中操作 ＞0为项目中操作
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 要删除的id
        /// </summary>
        public List<int> Ids { get; set; }

        /// <summary>
        /// 类型：1 预交底 2交底验收
        /// </summary>
        public int Type { get; set; }

    }
}
