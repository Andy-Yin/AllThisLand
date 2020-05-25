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
    public class ReqStopOrStartListForApp : ReqAuth
    {

        public int ProjectId { get; set; }

    }
}
