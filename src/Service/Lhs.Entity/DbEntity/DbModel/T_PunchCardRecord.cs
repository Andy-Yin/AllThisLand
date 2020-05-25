using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_PunchCardRecord
    /// </summary>
    [Table("T_PunchCardRecord")]
    public class T_PunchCardRecord
    {
        public T_PunchCardRecord()
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
        /// 类型：0 进场 1 离场
        /// </summary>
        public byte Type
        {
            get;
            set;
        }        

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId
        {
            get;
            set;
        }        

        /// <summary>
        /// 所在位置
        /// </summary>
        public string Location
        {
            get;
            set;
        }        

        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude
        {
            get;
            set;
        }        

        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude
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
