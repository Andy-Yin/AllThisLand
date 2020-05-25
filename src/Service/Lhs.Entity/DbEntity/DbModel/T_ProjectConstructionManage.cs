using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_ProjectConstructionManage
    /// </summary>
    [Table("T_ProjectConstructionManage")]
    public class T_ProjectConstructionManage
    {
        public T_ProjectConstructionManage()
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
        public int ProjectId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            get;
            set;
        }

        /// <summary>
        /// 派工状态：0 未开始 1进行中 2 已完成
        /// </summary>
        public short AssignWorkerStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 验收状态：0 未开始 1进行中 2 已完成
        /// </summary>
        public short CheckStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 借支状态：0 未开始 1进行中 2 已完成
        /// </summary>
        public short CashStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 结算状态：0 未开始 1进行中 2 已完成
        /// </summary>
        public short SettlementStatus
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
        public DateTime EditTime
        {
            get;
            set;
        } = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        } = DateTime.Now;
    }
}
