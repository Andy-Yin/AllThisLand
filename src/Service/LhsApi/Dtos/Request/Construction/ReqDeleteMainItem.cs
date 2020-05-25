using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 删除
    /// </summary>
    public class ReqDeleteMainItem : ReqAuth
    {
        /// <summary>
        /// 要删除的id
        /// </summary>
        public int Id { get; set; }

    }
}
