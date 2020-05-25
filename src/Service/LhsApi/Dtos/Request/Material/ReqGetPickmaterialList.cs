using System;
using System.Collections.Generic;
using System.Linq;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;
using LhsApi.Dtos.Response.Material;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetPickmaterialList : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
