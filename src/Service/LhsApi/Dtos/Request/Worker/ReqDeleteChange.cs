using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 删除工人变更
    /// </summary>
    public class ReqDeleteChange : ReqAuth
    {
        /// <summary>
        /// 要删除的id
        /// </summary>
        public List<int> Ids{ get; set; }

    }
}
