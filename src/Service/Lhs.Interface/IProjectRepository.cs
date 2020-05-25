using Core.Data;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Entity.ForeignDtos.Response.Project;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Common.Enum;

namespace Lhs.Interface
{
    public interface IProjectRepository : IPlatformBaseService<T_Project>
    {
        Task<List<ProjectUser>> GetProjectUserListByProjectId(int projectId);

        /// <summary>
        /// 根据报价单获取项目
        /// 需要外部判断是否为空
        /// </summary>
        Task<T_Project> GetProjectByQuotationId(string quotationId);

        Task<PageResponse<ProjectListResp>> GetProjectList(ProjectListRequ request);

        /// <summary>
        /// 获取客户的项目列表
        /// </summary>
        Task<List<ProjectListResp>> GetCustomerProjectList(int userId, string name);

        Task<List<PlanProjectList>> GetPlanProjectList(string no, int id);

        /// <summary>
        /// 报价单是否存在
        /// </summary>
        /// <param name="quotationId"></param>
        Task<bool> IsPackageExist(string quotationId);

        /// <summary>
        /// 审批流中的用户不存在的岗位
        /// </summary>
        Task<int> IsFlowPositionExist(string constructionMasterId, string solidDesignerId,
            string softDesignerId,string supervisionId);

        Task<bool> AddProject(AddProjectReq request);

        /// <summary>
        /// 保存项目的预交底 交底模板
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="type">1 预交底 2 交底</param>
        /// <param name="templateId"></param>
        Task<bool> SaveProjectDisclosure(int projectId, int type, int templateId);

        /// <summary>
        /// 保存项目的施工管理模板
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="templateId"></param>
        Task<bool> SaveProjectConstructionManage(int projectId, int templateId);

        /// <summary>
        /// 保存项目的施工计划模板
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="templateId"></param>
        Task<bool> SaveProjectConstructionPlan(int projectId, int templateId);

        /// <summary>
        /// 获取用户的项目列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="position">类型：1 工长 2 监理</param>
        /// <param name="name">搜索条件</param>
        /// <param name="pageNum">页码</param>
        /// <param name="status">项目状态：1 待开工 2 准备期 3 在建 4 已竣工 5 已停工</param>
        Task<PageResponse<UserProjectInfo>> GetUserProjectList(int userId, int position, string name, int pageNum, int status);

        /// <summary>
        /// 获取项目匹配的模板
        /// </summary>
        Task<List<MatchingTemplate>> GetMatchingTemplate(int projectId);

        /// <summary>
        /// 获取所有项目
        /// </summary>
        Task<List<T_Project>> GetAllProject();
    }
}
