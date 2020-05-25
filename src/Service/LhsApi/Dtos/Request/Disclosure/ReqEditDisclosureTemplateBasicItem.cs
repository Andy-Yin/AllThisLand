using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Disclosure
{
    /// <summary>
    /// 模板关联基础数据
    /// </summary>
    public class ReqEditDisclosureTemplateBasicItem : ReqAuth
    {
        /// <summary>
        /// 模板id 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 交底基础数据ids
        /// </summary>
        public List<int> PreDisclosureBasicIds { get; set; }

        /// <summary>
        /// 类型：1 预交底 2交底验收
        /// </summary>
        public int Type { get; set; }

    }
}
