using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_Department
    /// </summary>
    [Table("T_Department")]
    public class T_Department
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime EditTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }
    }
}
