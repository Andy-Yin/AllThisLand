namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetDetail : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目的物料Id
        /// </summary>
        public int ProjectMaterialItemId { get; set; }
    }
}
