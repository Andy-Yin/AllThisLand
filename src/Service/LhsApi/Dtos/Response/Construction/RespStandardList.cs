using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using NPOI.SS.UserModel;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 验收标准列表
    /// </summary>
    public class RespStandardList
    {
        public RespStandardList()
        {
        }

        public RespStandardList(T_ProjectConstructionCheckStandard standard)
        {
            StandardId = standard.Id;
            Name = standard.Name;
            Desc = standard.Content;
        }

        public RespStandardList(T_ConstructionManageCheckStandard standard)
        {
            StandardId = standard.Id;
            Name = standard.Name;
            Desc = standard.Content;
        }

        /// <summary>
        /// id
        /// </summary>
        public int StandardId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
