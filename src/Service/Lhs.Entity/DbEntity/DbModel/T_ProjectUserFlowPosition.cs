using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectUserFlowPosition
    /// </summary>
    [Table("T_ProjectUserFlowPosition")]
    public class T_ProjectUserFlowPosition
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public int FlowPositionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
