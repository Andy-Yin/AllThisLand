using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Setting
{
    /// <summary>
    /// 岗位授权
    /// </summary>
    public class ReqAuthorizePosition : ReqAuth
    {
        /// <summary>
        /// 岗位id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        public List<int> Values { get; set; }
    }
}
