using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectFlowRecord
    /// </summary>
    [Table("T_ProjectFlowRecord")]
    public class T_ProjectFlowRecord
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
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 类型：1 发包确认 2 预交底审批 3 订单采购审批 4 交底验收审批
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 节点id
        /// </summary>
        public int FlowNodeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FlowNodeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int FlowPositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FlowPositionName { get; set; }

        /// <summary>
        /// 审批结果：1 通过 2 驳回
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 备注或者驳回原因
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime CreateTime { get; set; }
    }
}
