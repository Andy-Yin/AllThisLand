using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using NPOI.SS.UserModel;

namespace LhsAPI.Dtos.Response.Construction
{
    /// <summary>
    /// 主材基础数据列表
    /// </summary>
    public class RespManageList
    {
        public RespManageList()
        {
        }

        public RespManageList(T_ConstructionManageCategory category, List<T_ConstructionManageTemplateItem> templateItem)
        {
            ItemId = category.Id;
            ItemName = category.Name;
            Selected = templateItem.Any(n => n.CategoryId == category.Id);
        }

        public RespManageList(T_ConstructionManageCategory category)
        {
            ItemId = category.Id;
            ItemName = category.Name;
        }

        /// <summary>
        /// id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; } = false;

        /// <summary>
        /// 任务列表
        /// </summary>
        public List<ManageTask> TaskList { get; set; }
    }

    public class ManageTask
    {
        public ManageTask()
        {
        }

        public ManageTask(T_ConstructionManageCheckTask task)
        {
            TaskId = task.Id;
            TaskName = task.Name;
        }

        /// <summary>
        /// id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string TaskName { get; set; }
    }
}
