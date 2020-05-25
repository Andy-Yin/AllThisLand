using Lhs.Entity.DbEntity.DbModel;
using NPOI.Util;

namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetLog : ReqAuth
    {
        /// <summary>
        /// 项目的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }
    }
}
