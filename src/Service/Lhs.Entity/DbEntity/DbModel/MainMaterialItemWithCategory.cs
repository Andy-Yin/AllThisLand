using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Entity.DbEntity.DbModel
{
    public class MainMaterialItemWithCategory
    {
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
        /// 所属类别
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }

        public string CategoryName { get; set; }

        /// <summary>
        /// 匹配规则:分隔符为|
        /// </summary>
        public string Code
        {
            get;
            set;
        }
    }
}
