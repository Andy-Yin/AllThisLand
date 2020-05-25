using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    /// <summary>
    /// 模板基础数据
    /// </summary>
    public class RespLocalBasicItem
    {
        public RespLocalBasicItem()
        {
        }

        public RespLocalBasicItem(LocalBasicItem itemInfo)
        {
            Id = itemInfo.Id;
            Name = itemInfo.Name;
            Code = itemInfo.Code.Split(CommonConst.Separator).ToList();
            //Selected = itemInfo.TemplateId > 0;
        }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public List<string> Code { get; set; }

        ///// <summary>
        ///// 是否被选中 todo：确定不用了就删掉
        ///// </summary>
        //public bool Selected { get; set; } = false;
    }
}
