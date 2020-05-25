using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Interface
{
    public interface IConstructionRepository : IPlatformBaseService<T_ProjectConstructionManage>
    {
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="ids">要删除的id </param>
        /// <param name="type">类型：3 施工管理 4 施工计划</param>
        Task<bool> DeleteTemplate(List<int> ids, int type);

        /// <summary>
        /// 获取施工列表
        /// </summary>
        /// <param name="projectId">项目id</param>
        Task<List<T_ProjectConstructionManage>> GetConstructionList(int projectId);

        /// <summary>
        /// 获取派工列表
        /// </summary>
        /// <param name="workType">后台施工管理里的分类/工种</param>
        /// <param name="pageNum">第几页</param>
        Task<PageResponse<WorkerInfo>> GetAssignWorkerList(int workType, int pageNum);

        /// <summary>
        /// 获取验收列表
        /// </summary>
        /// <param name="workType">后台施工管理里的分类/工种</param>
        /// <param name="pageNum">第几页</param>
        Task<PageResponse<CheckTask>> GetCheckList(int workType, int pageNum);

        /// <summary>
        /// 获取某个验收任务信息
        /// </summary>
        /// <param name="projectTaskId">机构验收任务id</param>
        Task<T_ProjectConstructionCheckTask> GetCheckTask(int projectTaskId);

        /// <summary>
        /// 获取某个验收任务的验收记录
        /// </summary>
        /// <param name="projectTaskId">机构验收任务id</param>
        Task<List<CheckRecordInfo>> GetCheckRecordList(int projectTaskId);

        /// <summary>
        /// 更新验收任务的状态
        /// </summary>
        /// <param name="projectTaskId">机构验收任务id</param>
        Task<bool> SaveCheckTaskStatus(int projectTaskId);

        /// <summary>
        /// 获取地采模板下的基础数据
        /// </summary>
        Task<List<LocalBasicItem>> GetLocalTemplateItemList(int templateId);

        /// <summary>
        /// 获取地采基础数据
        /// </summary>
        Task<List<LocalBasicItem>> GetLocalBasicItemList(string name);
    }
}
