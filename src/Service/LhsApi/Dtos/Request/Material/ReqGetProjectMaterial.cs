namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetProjectMaterial : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
