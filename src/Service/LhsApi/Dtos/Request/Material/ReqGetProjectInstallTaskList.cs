namespace LhsApi.Dtos.Request.Material
{
    public class ReqGetProjectInstallTaskList : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 二级分类的Id，如瓷砖
        /// </summary>
        public int SecondCategoryId { get; set; }
    }
}
