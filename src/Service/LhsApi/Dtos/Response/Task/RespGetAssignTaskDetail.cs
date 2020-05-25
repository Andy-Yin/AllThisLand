using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Task
{
    public class RespGetAssignTaskDetail
    {
        public RespGetAssignTaskDetail(T_ProjectAssignTask task)
        {
            TaskNo = task.TaskNo;
            TaskName = task.TaskName;
            WorkerId = task.WorkerId;
            WorkerName = task.WorkerName;
            Male = task.Male ? "男" : "女";
            Phone = task.Phone;
            CompanyName = task.CompanyName;
        }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string TaskNo { get; set; }

        public string TaskName { get; set; }

        /// <summary>
        /// 工人Id
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// 工人名字
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// 性别：1-男，2-女
        /// </summary>
        public string Male { get; set; }

        /// <summary>
        /// 工人手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 分公司名字
        /// </summary>
        public string CompanyName { get; set; }
    }
}
