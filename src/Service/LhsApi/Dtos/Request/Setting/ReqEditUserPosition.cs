using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Setting
{
    /// <summary>
    /// 用户编辑岗位
    /// </summary>
    public class ReqEditUserPosition : ReqAuth
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int editUserId { get; set; }

        /// <summary>
        /// 岗位id
        /// </summary>
        public List<int> PositionIds { get; set; }
    }
}
