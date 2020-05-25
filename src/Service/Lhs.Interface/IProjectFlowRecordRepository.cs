using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request;
using Lhs.Entity.ForeignDtos.Request.Project;

namespace Lhs.Interface
{
    ///<summary>
    ///表T_ProjectFlowRecord的接口
    ///</summary>
    public interface IProjectFlowRecordRepository : IPlatformBaseService<T_ProjectFlowRecord>
    {
        /// <summary>
        /// 是否满足开工要求
        /// </summary>
        Task<bool> CanStartProject(string projectNo, ReqAuth auth);

        /// <summary>
        /// 提交项目审批流审核
        /// </summary>
        Task<bool> SubmitFlowApprove(ReqFlowApprove flowApprove, string u9UserId, string quotationId, ReqAuth auth);

        /// <summary>
        /// 获取项目的审批记录
        /// </summary>
        Task<List<ProjectFlowRecord>> GetProjectFlowRecord(int projectId);

        /// <summary>
        /// 获取项目的待审批记录
        /// </summary>
        Task<CurrentProjectFlow> GetProjectCurrentFlow(int projectId);

    }
}
