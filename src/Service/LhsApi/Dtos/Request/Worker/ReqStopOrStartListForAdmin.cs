using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Lhs.Entity.DbEntity.DbModel;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 获取列表
    /// </summary>
    public class ReqStopOrStartListForAdmin : ReqAuth
    {
        [Range(0, 1)]
        public EnumStopWorkType Type { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartFromDay { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartEndDay { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StopFromDay { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StopToDay { get; set; }

        /// <summary>
        /// 项目编号，项目名称，工长名称
        /// </summary>
        public string Search { get; set; } = "";

        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 20;

    }
}
