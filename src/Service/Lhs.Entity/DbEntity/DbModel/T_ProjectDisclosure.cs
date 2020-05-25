using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameWork.Entity
{
    ///<summary>
    ///表T_ProjectDisclosure的实体类
    ///</summary>
    [Table("T_ProjectDisclosure")]
    public class T_ProjectDisclosure
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? DisclosureItemId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisclosureItemName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsDel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? EditTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

    }
}
