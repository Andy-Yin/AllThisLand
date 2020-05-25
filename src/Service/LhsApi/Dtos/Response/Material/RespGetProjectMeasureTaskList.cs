using System.Collections;

namespace LhsApi.Dtos.Response.Material
{
    public class MeasureTaskList
    {
        public MeasureTaskList(int status, string statusName)
        {
            Status = status;
            StatusName = statusName;
            ItemList = new ArrayList();
        }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 待验收 4 已取消 5 已完成
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 任务状态：1 待开工 2 已开工 3 待验收 4 已取消 5 已完成
        /// </summary>
        public string StatusName { get; set; }

        public ArrayList ItemList { get; set; }
    }
}
