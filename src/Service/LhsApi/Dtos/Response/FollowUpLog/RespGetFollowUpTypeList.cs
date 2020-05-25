using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    /// <summary>
    /// 模板基础数据
    /// </summary>
    public class RespGetFollowUpTypeList
    {
        public RespGetFollowUpTypeList()
        {
        }

        public RespGetFollowUpTypeList(T_FollowupType itemInfo)
        {
            Id = itemInfo.Id;
            Name = itemInfo.Name;
        }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
