using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_FollowupLog
    /// </summary>
    [Table("T_FollowupLog")]
    public class T_FollowupLog
    {
        public T_FollowupLog()
        {
        }

        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片地址：|分隔
        /// </summary>
        public string Imgs { get; set; }

        /// <summary>
        /// 跟进类型id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
