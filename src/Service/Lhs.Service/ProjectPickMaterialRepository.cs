using System;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Task;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Dapper;
using Dapper.Contrib.Extensions;
using Lhs.Common.Const;
using Lhs.Entity.ForeignDtos.Response.U9;
using Microsoft.Extensions.Configuration;
using U9Service;

namespace Lhs.Service
{
    /// <summary>
    /// 物料
    /// </summary>
    public class ProjectPickMaterialRepository : PlatformBaseService<T_ProjectPickMaterial>, IProjectPickMaterialRepository
    {
        public ProjectPickMaterialRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<List<T_ProjectPickMaterial>> GetProjectPickMaterialListByProjectId(int projectId)
        {
            string sql = $" SELECT * FROM T_ProjectPickMaterial WHERE ProjectId={projectId} AND IsDel = 0";
            var list = (await this.FindAllAsync<T_ProjectPickMaterial>(sql)).ToList();

            return list;
        }

        public async Task<bool> Pick(
            IProjectMaterialRepository projectMaterialRepository,
            IProjectMaterialItemLogRepository logRepository,
            T_ProjectPickMaterial projectPickMaterial,
            T_ProjectMaterialItem material,
            string timeSign,
            string key)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 更新U9审批流状态回写
                        var client = new JFClient();
                        var detailIdList = new List<string>();
                        detailIdList.Add(material.DetailsId);

                        // 更新物料
                        await projectMaterialRepository.UpdateAsync(conn, material, tran);
                        // 新增领料
                        var taskId = await AddAsync(conn, projectPickMaterial, tran);

                        // 添加log记录
                        var log = new T_ProjectMaterialItemLog();
                        log.CreateTime = DateTime.Now;
                        log.ProjectMaterialItemId = material.Id;
                        log.Remark = "提交领料任务";
                        log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), material.Status);
                        log.Status = material.Status;

                        await logRepository.AddAsync(conn, log, tran);

                        var response = await client.SetMaterialApply2Async(CommonConst.U9ServiceSource, timeSign, key, detailIdList.ToArray());
                        var u9Result = JsonHelper.DeserializeJsonToObject<JFSendProStatus>(response);
                        if (u9Result.errcode == 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        public async Task<bool> ConfirmReceive(
            IProjectMaterialRepository projectMaterialRepository,
            IProjectMaterialItemLogRepository logRepository,
            List<T_ProjectMaterialItem> materialList,
            List<T_ProjectPickMaterial> pickMaterialList,
            string timeSign,
            string key)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();

                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 更新U9审批流状态回写
                        var client = new JFClient();
                        var detailIdList = new List<string>();
                        // 更新物料
                        await projectMaterialRepository.UpdateListAsync(conn, materialList, tran);
                        // 更新领料
                        await UpdateListAsync(conn, pickMaterialList, tran);

                        foreach (var materialItem in materialList)
                        {
                            // 添加log记录
                            var log = new T_ProjectMaterialItemLog();
                            log.CreateTime = DateTime.Now;
                            log.ProjectMaterialItemId = materialItem.Id;
                            log.Remark = "提交安装任务";
                            log.StatusName = EnumHelper.GetDescription(typeof(EnumMaterialItemStatus), materialItem.Status);
                            log.Status = materialItem.Status;

                            await logRepository.AddAsync(conn, log, tran);

                            // 添加物料id
                            detailIdList.Add(materialItem.DetailsId);
                        }

                        var response = await client.SetConfirmationGoodsAsync(CommonConst.U9ServiceSource, timeSign, key, detailIdList.ToArray());
                        var u9Result = JsonHelper.DeserializeJsonToObject<JFSendProStatus>(response);
                        if (u9Result.errcode == 0)
                        {
                            tran.Commit();
                            return true;
                        }
                        else
                        {
                            tran.Rollback();
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
