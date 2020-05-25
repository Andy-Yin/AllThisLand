using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Common;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response
{
    public class UserPosition
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 岗位id
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PositionName { get; set; }
    }
}
