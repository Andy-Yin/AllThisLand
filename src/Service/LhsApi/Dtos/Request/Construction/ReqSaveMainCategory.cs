using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 保存主材基础数据分类
    /// </summary>
    public class ReqSaveMainCategory : ReqAuth
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
