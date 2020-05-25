using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Common;
using Lhs.Entity.DbEntity;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Entity.ForeignDtos.Response.Construction
{
    public class ProjectConstructionManage
    {
        /// <summary>
        /// 分类id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 任务id    
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 验收标准id
        /// </summary>
        public int StandardId { get; set; }

        /// <summary>
        /// 验收标准名称
        /// </summary>
        public string StandardName { get; set; }
    }
}
