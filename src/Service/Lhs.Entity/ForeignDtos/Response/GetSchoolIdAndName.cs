using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhs.Entity.ForeignDtos.Response
{
    /// <summary>
    /// 获取关联机构id和名称
    /// </summary>
    public class SchoolIdAndName
    {
        /// <summary>
        /// 关联机构id
        /// </summary>
        public int SchoolId { get; set; }

        /// <summary>
        /// 关联机构全称
        /// </summary>
        public string SchoolName { get; set; }
    }
}
