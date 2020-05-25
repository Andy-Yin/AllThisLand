using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lhs.Entity.DbEntity.DbModel
{
    /// <summary>
    /// 实体类：T_Menu
    /// </summary>
    [Table("T_Menu")]
    public class T_Menu
    {
        public T_Menu()
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
        /// 父菜单Id，没有父节点为0
        /// </summary>
        public int ParentId
        {
            get;
            set;
        }        

        /// <summary>
        /// 菜单对应的前端路由
        /// </summary>
        public string Url
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
