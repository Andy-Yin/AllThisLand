using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 删除类目
    /// </summary>
    public class ReqDeleteItems : ReqAuth
    {
        /// <summary>
        /// 项目id：0为基础数据中操作 ＞0为项目中操作
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 要删除的id
        /// </summary>
        public List<int> ItemIds { get; set; }
    }
}
