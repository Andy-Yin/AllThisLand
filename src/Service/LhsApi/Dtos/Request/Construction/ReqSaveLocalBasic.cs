using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存地采基础数据
    /// </summary>
    public class ReqSaveLocalBasic : ReqAuth
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public List<string> Code { get; set; }

    }
}
