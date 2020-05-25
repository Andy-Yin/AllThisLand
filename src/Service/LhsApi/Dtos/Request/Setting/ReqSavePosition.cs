using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Setting
{
    /// <summary>
    /// 新增、编辑岗位
    /// </summary>
    public class ReqSavePosition : ReqAuth
    {
        /// <summary>
        /// 岗位id
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
