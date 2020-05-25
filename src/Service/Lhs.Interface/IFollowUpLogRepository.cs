using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    /// <summary>
    /// 跟进日志相关
    /// </summary>
    public interface IFollowUpLogRepository : IPlatformBaseService<T_FollowupLog>
    {
        /// <summary>
        /// 添加一条日志
        /// </summary>
        Task<bool> AddFollowUpLog(T_FollowupLog log);

        /// <summary>
        /// 获取所有的跟进类别
        /// </summary>
        Task<List<T_FollowupType>> GetFollowUpTypeList();

        /// <summary>
        /// 获取某个人在一个项目里的跟进日志
        /// </summary>
        Task<List<T_FollowupLog>> GetFollowUpLogList(int userId, int projectId);

        /// <summary>
        /// 保存日志分类
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveFollowUpLogType(T_FollowupType item);

        /// <summary>
        /// 删除日志分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteFollowUpLogType(int id);
    }
}
