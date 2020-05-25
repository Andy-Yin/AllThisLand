using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace LhsApi.Dtos.Response.Task
{
    public class RespGetTaskItem
    {
        public RespGetTaskItem(int id, EnumTaskType type, string taskNo, string taskName, int materialId)
        {
            Id = id;
            Type = type;
            TaskNo = taskNo;
            TaskName = taskName;
            MaterialId = materialId;
        }

        public int Id { get; set; }

        /// <summary>
        /// 测量、安装、订单任务的物料id
        /// 派工任务没有物料id，=0
        /// </summary>
        public int MaterialId { get; set; }

        public EnumTaskType Type { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
        public string TaskNo { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

    }

    public enum EnumTaskType
    {
        /// <summary>
        /// 测量1
        /// </summary>
        Measure = 1,

        /// <summary>
        /// 订单2
        /// </summary>
        Order = 2,

        /// <summary>
        /// 安装3
        /// </summary>
        Install = 3,

        /// <summary>
        /// 派工4
        /// </summary>
        Assign = 4,
    }

}
