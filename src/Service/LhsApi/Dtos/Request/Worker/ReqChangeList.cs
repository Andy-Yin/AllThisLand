using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;
using LhsApi.Dtos.Response;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 获取列表
    /// </summary>
    public class ReqChangeList : ReqBasePage
    {

        /// <summary>
        /// 项目id：APP参数
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 申请类型： 0 全部 1 施工工人 2 安装工人
        /// </summary>
        public int ApplyType { get; set; }

        /// <summary>
        /// 搜索条件:申请编号/项目名称/工长名称
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 搜索条件：开始时间
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 搜索条件：结束时间
        /// </summary>
        public string EndTIme { get; set; }

    }
}
