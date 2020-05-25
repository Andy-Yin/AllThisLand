using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqInStorageFromU9 : ReqAuth
    {
        /// <summary>
        /// 报价单
        /// </summary>
        public string QuotationId { get; set; }

        public List<int> OrderTaskIdList { get; set; }
    }
}
