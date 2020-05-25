using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Setting
{
    /// <summary>
    /// 获取岗位的权限
    /// </summary>
    public class ReqGetAllPermissionForPosition : ReqAuth
    {
        /// <summary>
        /// 岗位id
        /// </summary>
        public int PositionId { get; set; }
    }
}
