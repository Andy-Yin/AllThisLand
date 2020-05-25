namespace LhsApi.Dtos.Request.Task
{
    public class ReqGetTaskList : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 工长的用户Id
        /// </summary>
        public int UserId { get; set; }
    }
}
