using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.User
{
    public class RespListUserInfo
    {
        public RespListUserInfo()
        {
        }

        public RespListUserInfo(UserInfo user, List<UserPosition> userPositions)
        {
            Id = user.Id;
            UserName = user.Name;
            Sex = user.Sex ? "女" : "男";
            Phone = user.Phone;
            RegionName = user.RegionName;
            CompanyId = user.CompanyId ?? string.Empty;
            DepartmentId = user.DepartmentId ?? string.Empty;
            CompanyName = user.CompanyName ?? string.Empty;
            DepartmentName = user.DepartmentName ?? string.Empty;
            IsUsed = user.IsUsed;
            PositionList = userPositions.Where(n => n.UserId == user.Id)
                .Select(r => new Position() { PositionId = r.PositionId, PositionName = r.PositionName }).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegionName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string CompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 岗位列表
        /// </summary>
        public List<Position> PositionList { get; set; } = new List<Position>();
    }

    public class Position
    {
        public Position()
        {
        }

        public Position(T_Position position)
        {
            PositionId = position.Id;
            PositionName = position.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PositionName { get; set; } = string.Empty;
    }
}
