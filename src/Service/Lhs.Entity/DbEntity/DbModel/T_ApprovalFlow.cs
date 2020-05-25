using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ApprovalFlow
    /// </summary>
    [Table("T_ApprovalFlow")]
    public class T_ApprovalFlow
    {
        public T_ApprovalFlow()
        {
        } 
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }        

        /// <summary>
        /// 类型：1 发包确认 2 预交底确认 3 订单采购确认 4 交底验收确认
        /// </summary>
        public short Type
        {
            get;
            set;
        }        

        /// <summary>
        /// 当前审批节点
        /// </summary>
        public int ApprovalLevel
        {
            get;
            set;
        }        

        /// <summary>
        /// 下一个审批节点
        /// </summary>
        public int NextLevel
        {
            get;
            set;
        }        

        /// <summary>
        /// 审批人：岗位/角色id
        /// </summary>
        public int PositonId
        {
            get;
            set;
        }        

        /// <summary>
        /// 
        /// </summary>
        public bool IsDel
        {
            get;
            set;
        }        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            get;
            set;
        }        
    }
}
