using System;
using System.Collections.Generic;
using System.Text;
using Lhs.Common;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response
{
    public class UserInfo : T_User
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; } = string.Empty;

        /// <summary>
        /// 分公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
