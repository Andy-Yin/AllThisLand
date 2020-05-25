using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Setting
{
    /// <summary>
    /// 删除岗位
    /// </summary>
    public class ReqDeletePosition : ReqAuth
    {
        /// <summary>
        /// 眼删除的id
        /// </summary>
        public List<int> Ids { get; set; }

    }
}
