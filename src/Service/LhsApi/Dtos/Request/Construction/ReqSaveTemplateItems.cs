using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Disclosure
{
    /// <summary>
    /// 保存模板的基础数据
    /// </summary>
    public class ReqSaveTemplateItems : ReqAuth
    {
        /// <summary>
        /// 模板id
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// 基础数据id
        /// </summary>
        public List<int> ItemIds { get; set; }

    }
}
