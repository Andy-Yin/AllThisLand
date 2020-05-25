using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    /// <summary>
    /// 模板基础数据
    /// </summary>
    public class RespDisclosureItem
    {
        public RespDisclosureItem()
        {
        }

        public RespDisclosureItem(DisclosureItemInfo itemInfo)
        {
            Id = itemInfo.Id;
            Name = itemInfo.Name;
            Remark = itemInfo.Remark;
            Selected = itemInfo.TemplateId > 0;
        }

        public RespDisclosureItem(T_DisclosureItem template)
        {
            Id = template.Id;
            Name = template.Name;
            Remark = template.Remark;
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool Selected { get; set; } = false;
    }
}
