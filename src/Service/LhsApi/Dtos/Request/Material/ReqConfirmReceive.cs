using System;
using System.Collections.Generic;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Request.Material
{
    /// <summary>
    /// 签收物料
    /// </summary>
    public class ReqConfirmReceive : ReqAuth
    {
        /// <summary>
        /// APP系统内领料单的id
        /// </summary>
        public List<int> PickIdList { get; set; }
    }
}
