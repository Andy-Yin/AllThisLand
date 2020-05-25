using System;

namespace Lhs.Entity.DbEntity
{
    /// <summary>
    /// 管理类的实体基类，包括维护字段
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 创建者id
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 编辑者id
        /// </summary>
        public int EditUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime EditTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
    }
}
