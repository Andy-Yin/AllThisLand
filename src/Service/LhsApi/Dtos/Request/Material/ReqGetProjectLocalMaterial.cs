namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetProjectLocalMaterial : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}
