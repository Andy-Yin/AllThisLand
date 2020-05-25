using System.Collections.Generic;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    /// <summary>
    /// 模板基础数据
    /// </summary>
    public class RespGetFollowUpLogList
    {
        public RespGetFollowUpLogList()
        {
        }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名称：格式 交付质检员-张三
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 施工阶段：格式 漆工阶段-施工第2天
        /// </summary>
        public string ProjectStage { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
