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
    public class ReqWorkerList : ReqAuth
    {

        /// <summary>
        /// 分公司id
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 工种id
        /// </summary>
        public int WorkTypeId { get; set; }

        /// <summary>
        /// 工人姓名/手机号
        /// </summary>
        public string KeyWords { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 类型：1施工工人/2安装工人
        /// </summary>
        public int Type { get; set; }
    }
}
