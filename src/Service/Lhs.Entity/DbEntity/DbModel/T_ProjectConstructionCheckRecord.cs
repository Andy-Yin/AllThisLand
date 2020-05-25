using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectConstructionCheckRecord
    /// </summary>
    [Table("T_ProjectConstructionCheckRecord")]
    public class T_ProjectConstructionCheckRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        public int ProjectTaskId { get; set; }

        /// <summary>
        /// 验收结果：0 退回 1同意
        /// </summary>
        public bool? Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片地址：|分隔
        /// </summary>
        public string Imgs { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }

    }
}
