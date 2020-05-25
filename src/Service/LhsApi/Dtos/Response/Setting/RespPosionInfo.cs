using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.Setting
{
    public class RespPosionInfo
    {
        public RespPosionInfo()
        {
        }

        public RespPosionInfo(T_Position position)
        {
            Id = position.Id;
            Name = position.Name;
            Desc = position.Remark ?? string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;


        /// <summary>
        /// 
        /// </summary>
        public string Desc { get; set; } = string.Empty;
    }
}
