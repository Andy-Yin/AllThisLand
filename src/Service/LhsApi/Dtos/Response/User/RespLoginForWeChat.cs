using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response;

namespace LhsAPI.Dtos.Response.User
{
    //todo:返回的用户信息字段待定
    public class RespLoginForWeChat
    {
        public RespLoginForWeChat()
        {
        }

        public RespLoginForWeChat(T_Customer userInfo)
        {
            UserId = userInfo.Id;
            UserName = userInfo.Name;
        }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
