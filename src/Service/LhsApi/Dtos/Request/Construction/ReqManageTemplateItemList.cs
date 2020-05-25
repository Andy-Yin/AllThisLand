using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 获取模板内容
    /// </summary>
    public class ReqManageTemplateItemList : ReqAuth
    {
        /// <summary>
        /// 模板id
        /// </summary>
        public int ManageTemplateId { get; set; }

    }
}
