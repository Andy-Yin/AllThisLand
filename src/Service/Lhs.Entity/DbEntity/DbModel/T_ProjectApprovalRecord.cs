using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectApprovalRecord
    /// </summary>
    [Table("T_ProjectApprovalRecord")]
    public class T_ProjectApprovalRecord
    {
        public T_ProjectApprovalRecord()
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
        /// 项目编号
        /// </summary>
        public int ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 审批人
        /// </summary>
        public int UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 审批类型：
        ///1 发包确认（工长）
        ///2 预交底（家装设计师-主管-监理-家居设计师-主管-工长） 
        ///3 采购下单 （家装设计师-主管-家居设计师-主管-工长-工程助理-工程部长） 
        ///4 交底验收（工长-监理-客户）
        /// </summary>
        public short FollowId
        {
            get;
            set;
        }

        /// <summary>
        /// 审批状态：1 待审批 2 通过 3 驳回
        /// </summary>
        public short Status
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Remark
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
