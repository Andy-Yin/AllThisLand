using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_SignCheckRecord
    /// </summary>
    [Table("T_SignCheckRecord")]
    public class T_SignCheckRecord
    {
        public T_SignCheckRecord()
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
        public int SignRecordId
        {
            get;
            set;
        }        

        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialNo
        {
            get;
            set;
        }        

        /// <summary>
        /// 签收数量
        /// </summary>
        public int Count
        {
            get;
            set;
        }        

        /// <summary>
        /// 图片地址：|分隔
        /// </summary>
        public string Imgs
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
