using System;
using System.Collections.Generic;
using System.Linq;
using Lhs.Entity.DbEntity.DbModel;
using LhsAPI.Controllers;
using LhsApi.Dtos.Response.Material;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetInstallTaskDetail : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }
    }
}
