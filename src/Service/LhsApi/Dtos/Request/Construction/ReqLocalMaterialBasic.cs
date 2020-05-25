using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using LhsApi.Dtos.Request;

namespace LhsAPI.Dtos.Request.Construction
{
    /// <summary>
    /// 基础数据
    /// </summary>
    public class ReqLocalMaterialBasic : ReqAuth
    {
        ///// <summary>
        ///// 项目id todo：确定不用了就删除
        ///// </summary>
        //public int ProjectId { get; set; }

        ///// <summary>
        ///// 模板id  todo：确定不用了就删除
        ///// </summary>
        //public int TemplateId { get; set; }

        /// <summary>
        /// 搜索条件：名称 
        /// </summary>
        public string Name { get; set; }

    }
}
