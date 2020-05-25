using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Interface
{
    /// <summary>
    /// 质检管理相关
    /// </summary>
    public interface IProjectQualityRecordRepository : IPlatformBaseService<T_ProjectQualityRecord>
    {
        /// <summary>
        /// 质检管理列表
        /// </summary>
        Task<List<T_ProjectQualityRecord>> GetProjectQualityRecordListByProjectId(int projectId);

        /// <summary>
        /// 后台搜索某个监理所有的质检记录
        /// </summary>
        /// <returns></returns>
        Task<PageResponse<ProjectQualityRecordForAdmin>> GetPagedProjectQualityRecordList(
            int userId,
            EnumApprovalResult status,
            int pageNum,
            string minDate,
            string maxDate,
            string searchKey);

        /// <summary>
        /// 监理新增一条质检条目
        /// </summary>
        Task<bool> AddQualityRecordWhenFirst(T_ProjectQualityRecord record, T_ProjectQualityApprovalRecord approval);

        /// <summary>
        /// 审批记录
        /// </summary>
        /// <returns></returns>
        Task<List<T_ProjectQualityApprovalRecord>> GetApprovedList(int recordId);

        /// <summary>
        /// 审批过程
        /// </summary>
        /// <returns></returns>
        Task<bool> AddApproval(T_ProjectQualityRecord record, T_ProjectQualityApprovalRecord approval);

        /// <summary>
        /// 质检管理分类列表
        /// 如果没有上级目录，parentId=0，否则是上级的Id
        /// </summary>
        /// <returns></returns>
        Task<List<T_QualityItemCategory>> GetQualityItemCategoryList(int parentId, string search = "");

        /// <summary>
        /// 编辑质检管理分类
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveQualityItemCategory(T_QualityItemCategory category);

        /// <summary>
        /// 删除质检分类
        /// 需要判断有没有下级，如果有下级则不能删除，返回false
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteQualityCategory(int id);

        /// <summary>
        /// 获取某个分类下的质检条目
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<T_QualityItem>> GetQualityItemListByCategoryId(int categoryId);

        /// <summary>
        /// 获取某个条目的一级分类信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T_QualityItemCategory> GetRootCategoryByItemId(int id);

        /// <summary>
        /// 获取某一条一级分类或者二级分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T_QualityItemCategory> GetQualityItemCategoryById(int id);

        /// <summary>
        /// 获取某一个条目
        /// </summary>
        /// <returns></returns>
        Task<T_QualityItem> GetQualityItemById(int Id);

        /// <summary>
        /// 保存某个质检条目
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveQualityItem(T_QualityItem item);

        /// <summary>
        /// 删除某个质检条目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteQualityItem(int id);
    }
}
