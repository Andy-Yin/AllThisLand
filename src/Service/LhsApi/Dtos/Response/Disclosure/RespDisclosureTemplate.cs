using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;

namespace LhsAPI.Dtos.Response.Disclosure
{
    /// <summary>
    /// 模板列表信息
    /// </summary>
    public class RespTemplate
    {
        public RespTemplate()
        {
        }

        public RespTemplate(T_DisclosureTemplate template)
        {
            Id = template.Id;
            Name = template.Name;
            Remark = template.Remark;
        }

        public RespTemplate(TemplateInfo template)
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
        public string Remark  { get; set; }
    }
}
