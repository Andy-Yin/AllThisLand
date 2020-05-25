using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response.Construction
{
    public class CheckTask : T_ProjectConstructionCheckTask
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 工人名称
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// 工人电话
        /// </summary>
        public string WorkerPhone { get; set; }
    }
}
