using System;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    /// <summary>
    /// 主材管理
    /// </summary>
    public class ProjectMaterialRepository : PlatformBaseService<T_ProjectMaterialItem>, IProjectMaterialRepository
    {
        public ProjectMaterialRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectMaterialItem>> GetProjectMaterialItemListByProjectIdAndSecondCategoryId(int projectId, int secondCategoryId, EnumMaterialType type = EnumMaterialType.Main)
        {
            string sql = $" SELECT * FROM T_ProjectMaterialItem WHERE ProjectId={projectId} AND TYPE = {(int) type} AND SecondCategoryId = {secondCategoryId} and IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectMaterialItem>(sql)).ToList();

            return list;
        }

        public async Task<T_ProjectMaterialItem> GetProjectMaterialItemByNoAndSpace(int projectId, string number, string space)
        {
            string sql = $" SELECT * FROM T_ProjectMaterialItem WHERE ProjectId={projectId} AND MaterialNo = {number} AND space = {space} and IsDel = 0";
            var itemList = (await this.AllAsync(sql));
            if (itemList.Any())
            {
                return itemList.First();
            }
            else
            {
                return null;
            }

        }

        public async Task<List<T_ProjectMaterialItem>> GetProjectMaterialItemListByProjectId(int projectId, EnumMaterialType type)
        {
            string sql = $" SELECT * FROM T_ProjectMaterialItem WHERE ProjectId={projectId} AND TYPE = {(int) type} and IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectMaterialItem>(sql)).ToList();

            return list;
        }

        public async Task<List<T_ProjectMaterialItem>> GetProjectMaterialItemListByProjectId(int projectId)
        {
            string sql = $" SELECT * FROM T_ProjectMaterialItem WHERE ProjectId={projectId} and IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectMaterialItem>(sql)).ToList();

            return list;
        }

        public async Task<bool> UpdateMaterialItemStatus(int itemId, EnumMaterialItemStatus status)
        {
            var item = await this.SingleAsync(itemId);

            item.Status = status;
            item.EditTime = DateTime.Now;

            return await this.UpdateAsync(item);
        }

        public async Task<bool> SyncMaterialFromU9(
            IProjectMaterialItemLogRepository logRepository,
            IProjectMaterialLabourRepository labourRepository,
            List<T_ProjectMaterialLabour> labourList,
            List<T_ProjectMaterialItem> insertItems,
            List<T_ProjectMaterialItem> updateItems,
            List<T_ProjectMaterialItem> deleteItems)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                using (var t = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var insertItem in insertItems)
                        {
                            var id = await AddAsync(conn, insertItem, t);
                            insertItem.Id = id;

                            var log = new T_ProjectMaterialItemLog();
                            log.CreateTime = DateTime.Now;
                            log.ProjectMaterialItemId = id;
                            log.Remark = "新增物料";
                            log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), insertItem.Status);
                            log.Status = insertItem.Status;

                            await logRepository.AddAsync(conn, log, t);
                        }

                        foreach (var updateItem in updateItems)
                        {
                            await UpdateAsync(conn, updateItem, t);
                        }

                        foreach (var deleteItem in deleteItems)
                        {
                            string sql = @$"
update T_ProjectMaterialItem set IsDel = 1 where MaterialNo = @MaterialNo and Space = @Space and ProjectId = @ProjectId;
update T_ProjectMeasureTask
    set IsDel = 1
    where ProjectMaterialItemId in
        (select id from T_ProjectMaterialItem where MaterialNo = @MaterialNo and Space = @Space and ProjectId = @ProjectId)
update T_ProjectOrderTask
    set IsDel = 1
    where ProjectMaterialItemId in
        (select id from T_ProjectMaterialItem where MaterialNo = @MaterialNo and Space = @Space and ProjectId = @ProjectId)
update T_ProjectInstallTask
    set IsDel = 1
    where ProjectMaterialItemId in
        (select id from T_ProjectMaterialItem where MaterialNo = @MaterialNo and Space = @Space and ProjectId = @ProjectId)
";
                            await conn.ExecuteAsync(sql, new {deleteItem.MaterialNo, deleteItem.Space, deleteItem.ProjectId}, t);
                        }

                        // 增加人工费用
                        await labourRepository.AddListAsync(conn, labourList, t);
                        
                        // 提交数据
                        t.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        t.Rollback();
                        return false;
                    }
                }
            }
        }

        public async Task<bool> ConfirmSettlementFromU9(int projectId)
        {
            string sql = $@"update T_ProjectMaterialItem set Status = {(int) EnumMaterialItemStatus.ToBeSettlement9} where ProjectId = {projectId}";
            return await ExecuteAsync(sql) > 0;
        }

        public Task<bool> DeleteMaterial(
            int materialId,
            IProjectMeasureTaskRepository measureTaskRepository,
            IProjectOrderTaskRepository orderTaskRepository,
            IProjectPickMaterialRepository pickMaterialRepository,
            IProjectInstallTaskRepository installTaskRepository,
            SqlConnection conn,
            SqlTransaction tran)
        {
            throw new NotImplementedException();
        }
    }
}
