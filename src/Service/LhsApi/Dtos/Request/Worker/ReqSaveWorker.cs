using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Worker
{
    /// <summary>
    /// 保存工人
    /// </summary>
    public class ReqSaveWorker : ReqAuth
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 所属分公司
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int WorkTypeId { get; set; }

    }
}
