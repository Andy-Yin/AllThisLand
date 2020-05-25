using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Project
{
    /// <summary>
    /// 用户的项目
    /// </summary>
    public class ReqUserProject : ReqBasePage
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 项目状态：1 待开工 2 准备期 3 在建 4 已竣工 5 已停工
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
    }
}
