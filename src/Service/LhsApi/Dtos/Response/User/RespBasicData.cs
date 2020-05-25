using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.User
{
    public class RespBasicData
    {
        /// <summary>
        /// 大区列表
        /// </summary>
        public List<Region> RegionList { get; set; } = new List<Region>();

        /// <summary>
        /// 岗位列表
        /// </summary>
        public List<Position> PositionList { get; set; } = new List<Position>();
    }

    public class Region
    {
        private string _regionName;

        /// <summary>
        /// 
        /// </summary>
        public string RegionName
        {
            get
            {
                if (string.IsNullOrEmpty(_regionName))
                {
                    return UserConst.RegionDefault;
                }
                return _regionName;
            }
            set => _regionName = value;
        }

        /// <summary>
        /// 分公司列表
        /// </summary>
        public List<Company> CompanyList { get; set; } = new List<Company>();
    }

    public class Company
    {
        public Company()
        {
        }

        public Company(T_Company company)
        {
            CompanyId = company.CompanyId;
            CompanyName = company.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 部门列表
        /// </summary>
        public List<Department> DepartmentList { get; set; } = new List<Department>();
    }

    public class Department
    {
        public Department()
        {
        }

        public Department(T_Department department)
        {
            DepartmentId = department.DepartmentId;
            DepartmentName = department.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;
    }
}
