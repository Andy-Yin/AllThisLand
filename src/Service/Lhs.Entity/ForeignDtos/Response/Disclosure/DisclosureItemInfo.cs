using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;

namespace Lhs.Entity.ForeignDtos.Response.Disclosure
{
    public class DisclosureItemInfo
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 模板id
        /// </summary>
        public int TemplateId { get; set; }
    }
}
