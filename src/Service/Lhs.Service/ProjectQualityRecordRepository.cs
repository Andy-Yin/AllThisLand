using System;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.ForeignDtos.Response.Worker;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    /// <summary>
    /// 基类只有一个安装任务吧先
    /// </summary>
    public class ProjectQualityRecordRepository : PlatformBaseService<T_ProjectQualityRecord>, IProjectQualityRecordRepository
    {
        public ProjectQualityRecordRepository(IConfiguration config)
        {
            Config = config;
        }
        public async Task<List<T_ProjectQualityRecord>> GetProjectQualityRecordListByProjectId(int projectId)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_ProjectQualityRecord WHERE IsDel=0  and ProjectId = {0} ", projectId);
                return (await conn.QueryAsync<T_ProjectQualityRecord>(sql)).ToList();
            }
        }

        public async Task<PageResponse<ProjectQualityRecordForAdmin>> GetPagedProjectQualityRecordList(
            int userId,
            EnumApprovalResult approvalResult,
            int pageNum,
            string minDate,
            string maxDate,
            string searchKey)
        {
            using (var coon = Connection)
            {
                var where = string.Empty;
                var startDate = DateTime.Now;
                var endDate = DateTime.Now;


                if (!string.IsNullOrEmpty(searchKey))
                {
                    where += @"
                          AND
                          (
                              p.ProjectNo like @searchKey or p.ProjectName like @searchKey
                          )";
                }

                if (!string.IsNullOrEmpty(minDate))
                {
                    startDate = DateTime.Parse(minDate).Date;
                    where += @" AND pqr.CreateTime > @startDate";
                }

                if (!string.IsNullOrEmpty(maxDate))
                {
                    endDate = DateTime.Parse(maxDate).Date.AddDays(1);
                    where += @" AND pqr.CreateTime < @endDate";
                }

                var sql = $@"
select pqr.*,
       p.ProjectNo,
       p.ProjectName,
       (select UserName
        from T_ProjectUserFlowPosition
        where ProjectId = p.Id and FlowPositionId = 1)  ConstructionManagerName, --工长
       (select UserName
        from T_ProjectUserFlowPosition
        where ProjectId = p.Id and FlowPositionId = 4)  SupervisorName           --监理
from T_ProjectQualityRecord pqr
         left join t_project p on p.Id = pqr.ProjectId
         left join T_ProjectUserFlowPosition pufp on p.Id = pufp.ProjectId
where pqr.Status = 3
  and ApprovalResult = @approvalResult
  and p.IsDel = 0
  and pufp.FlowPositionId = 4
  and pufp.UserId = @userId
                          {where}";
                return await PagedAsync<ProjectQualityRecordForAdmin>(sql, new
                    {
                        searchKey = $"%{searchKey}%",
                        startDate,
                        endDate,
                        userId,
                        approvalResult
                    }, "", pageNum,
                    CommonConst.PageSize);
            }
        }

        public async Task<bool> AddQualityRecordWhenFirst(T_ProjectQualityRecord record, T_ProjectQualityApprovalRecord approval)
        {
            using (var conn = Connection)
            {
                int id = await conn.InsertAsync(record);

                approval.QualityRecordId = id;

                await conn.InsertAsync(approval);

                return true;
            }
        }

        public async Task<List<T_ProjectQualityApprovalRecord>> GetApprovedList(int recordId)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_ProjectQualityApprovalRecord WHERE qualityRecordId = {0} ", recordId);
                return (await conn.QueryAsync<T_ProjectQualityApprovalRecord>(sql)).ToList();
            }
        }

        public async Task<bool> AddApproval(T_ProjectQualityRecord record, T_ProjectQualityApprovalRecord approval)
        {
            using (var conn = Connection)
            {
                await conn.UpdateAsync(record);
                return (await conn.InsertAsync(approval)) > 0;
            }
        }

        public async Task<List<T_QualityItemCategory>> GetQualityItemCategoryList(int parentId, string search = "")
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_QualityItemCategory WHERE IsDel=0  and parentId = {0} ", parentId);
                if (!string.IsNullOrEmpty(search))
                {
                    sql += string.Format(" and name like '%{0}%'", search);
                }
                return (await conn.QueryAsync<T_QualityItemCategory>(sql)).ToList();
            }
        }

        public async Task<bool> SaveQualityItemCategory(T_QualityItemCategory category)
        {
            using (var conn = Connection)
            {
                if (category.Id > 0)
                {
                    return (await conn.UpdateAsync(category));
                }

                return (await conn.InsertAsync(category)) > 0;
            }
        }

        public async Task<bool> DeleteQualityCategory(int id)
        {
            using (var conn = Connection)
            {
                var category = await conn.GetAsync<T_QualityItemCategory>(id);

                if (category == null)
                {
                    return true;
                }
                // 如果是一级分类
                if (category.ParentId == 0)
                {
                    // 查询二级分类个数
                    var count = (await GetQualityItemCategoryList(id)).Count;
                    if (count>0)
                    {
                        return false;
                    }
                }
                else
                {
                    //查询子项目的个数
                    var count = (await GetQualityItemListByCategoryId(id)).Count;
                    if (count>0)
                    {
                        return false;
                    }
                }

                var sql = string.Format(@"update T_QualityItemCategory set IsDel = 1 where id = {0} ", id);
                return (await conn.ExecuteAsync(sql)) > 0;
            }
        }

        public async Task<List<T_QualityItem>> GetQualityItemListByCategoryId(int categoryId)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_QualityItem WHERE IsDel=0  and CategoryId = {0} ", categoryId);
                return (await conn.QueryAsync<T_QualityItem>(sql)).ToList();
            }
        }

        public async Task<T_QualityItemCategory> GetRootCategoryByItemId(int id)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"
select *
from T_QualityItemCategory
where id in (
    select qic.ParentId
    from T_QualityItemCategory qic
             left join T_QualityItem qi on qic.id = qi.CategoryId
    where qi.Id ={0}) ", id);
                return await conn.QuerySingleAsync< T_QualityItemCategory>(sql);
            }
        }

        public async Task<T_QualityItemCategory> GetQualityItemCategoryById(int id)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_QualityItemCategory WHERE IsDel=0  and Id = {0} ", id);
                return await conn.QuerySingleAsync<T_QualityItemCategory>(sql);
            }
        }

        public async Task<T_QualityItem> GetQualityItemById(int id)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"SELECT * FROM dbo.T_QualityItem WHERE IsDel=0  and Id = {0} ", id);
                return await conn.QuerySingleAsync<T_QualityItem>(sql);
            }
        }

        public async Task<bool> SaveQualityItem(T_QualityItem item)
        {
            using (var conn = Connection)
            {
                if (item.Id > 0)
                {
                    return (await conn.UpdateAsync(item));
                }

                return (await conn.InsertAsync(item)) > 0;
            }
        }

        public async Task<bool> DeleteQualityItem(int id)
        {
            using (var conn = Connection)
            {
                var sql = string.Format(@"update T_QualityItem set IsDel = 1 where id = {0} ", id);
                return (await conn.ExecuteAsync(sql))>0;
            }
        }
    }
}
