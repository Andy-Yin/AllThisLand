using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Util;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Entity.ForeignDtos.Response.U9;
using U9Service;

namespace Lhs.Service
{
    /// <summary>
    /// 表T_ProjectFlowRecord的Service
    /// </summary>
    public class ProjectFlowRecordRepository : PlatformBaseService<T_ProjectFlowRecord>, IProjectFlowRecordRepository
    {
        public ProjectFlowRecordRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 是否满足开工要求
        /// </summary>
        public async Task<bool> CanStartProject(string projectNo, ReqAuth auth)
        {
            var client = new JFClient();
            var response = await client.GetJFAPStatusAsync(CommonConst.U9ServiceSource, auth.TimeSign, auth.Key, projectNo);
            var u9Result = JsonHelper.DeserializeJsonToObject<JFSendProStatus>(response);
            if (u9Result.errcode == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 项目审批流审核
        /// </summary>
        public async Task<bool> SubmitFlowApprove(ReqFlowApprove flowApprove, string u9UserId, string quotationId, ReqAuth auth)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                using (var t = conn.BeginTransaction())
                {
                    try
                    {
                        var updateFollowIdSql = flowApprove.Approved == (int)ApproveEnum.ProjectFlowStatus.Fail ? @";
                            --审核驳回，寻找上一个节点
                            WITH cte
                            AS (SELECT *
                                FROM dbo.T_FlowNode WITH (NOLOCK)
                                WHERE Id =
                                (
                                    SELECT FollowId FROM dbo.T_Project WITH (NOLOCK) WHERE Id = @ProjectId
                                )
                                UNION ALL
                                SELECT f.*
                                FROM cte
                                    INNER JOIN dbo.T_FlowNode f WITH (NOLOCK)
                                        ON cte.PreNodeId = f.Id)
                            UPDATE dbo.T_Project
                            SET FollowId =
                                (
                                    SELECT CASE
                                               WHEN EXISTS
                                                    (
                                                        SELECT cte.Id
                                                        FROM cte
                                                            INNER JOIN dbo.T_ProjectUserFlowPosition fp WITH (NOLOCK)
                                                                ON cte.FlowPositionId = fp.FlowPositionId
                                                                   AND fp.ProjectId = @ProjectId
                                                                   AND
                                                                   (
                                                                       cte.Id !=
                                                                   (
                                                                       SELECT FollowId FROM dbo.T_Project WITH (NOLOCK) WHERE Id = @ProjectId
                                                                   )
                                                                       OR cte.PreNodeId = 0
                                                                   ) --驳回到提交人，不可继续驳回
                                                    ) THEN
                                               (
                                                   SELECT TOP (1)
                                                          cte.Id
                                                   FROM cte
                                                       INNER JOIN dbo.T_ProjectUserFlowPosition fp WITH (NOLOCK)
                                                           ON cte.FlowPositionId = fp.FlowPositionId
                                                              AND fp.ProjectId = @ProjectId
                                                              AND
                                                              (
                                                                  cte.Id !=
                                                              (
                                                                  SELECT FollowId FROM dbo.T_Project WITH (NOLOCK) WHERE Id = @ProjectId
                                                              )
                                                                  OR cte.PreNodeId = 0
                                                              )
                                                   ORDER BY cte.Id DESC
                                               )
                                               ELSE
                                                   1 -- 找不到上个节点时，代表发包驳回
                                           END
                                ),
                                EditTime = GETDATE()
                            WHERE T_Project.Id = @ProjectId;" :
                            @";
                            --审核通过，寻找下一个节点
                            WITH cte
                            AS (SELECT *
                                FROM dbo.T_FlowNode WITH (NOLOCK)
                                WHERE Id =
                                (
                                    SELECT FollowId FROM dbo.T_Project WITH (NOLOCK) WHERE Id = @ProjectId
                                )
                                UNION ALL
                                SELECT f.*
                                FROM cte
                                    INNER JOIN dbo.T_FlowNode f WITH (NOLOCK)
                                        ON cte.NextNodeId = f.Id)
                            UPDATE dbo.T_Project
                            SET FollowId =
                                (
                                    SELECT CASE
                                               WHEN EXISTS
                                                    (
                                                        SELECT cte.Id
                                                        FROM cte
                                                            INNER JOIN dbo.T_ProjectUserFlowPosition fp WITH (NOLOCK)
                                                                ON cte.FlowPositionId = fp.FlowPositionId
                                                                   AND fp.ProjectId = @ProjectId
                                                                   AND cte.Id !=
                                                                   (
                                                                       SELECT FollowId FROM dbo.T_Project WITH (NOLOCK) WHERE Id = @ProjectId
                                                                   )
                                                    ) THEN
                                               (
                                                   SELECT TOP (1)
                                                          cte.Id
                                                   FROM cte
                                                       INNER JOIN dbo.T_ProjectUserFlowPosition fp WITH (NOLOCK)
                                                           ON cte.FlowPositionId = fp.FlowPositionId
                                                              AND fp.ProjectId = @ProjectId
                                                              AND cte.Id !=
                                                              (
                                                                  SELECT FollowId FROM dbo.T_Project WITH (NOLOCK) WHERE Id = @ProjectId
                                                              )
                                                   ORDER BY cte.Id
                                               )
                                               ELSE
                                                   0 -- 找不到下一个节点时，代表审核已完成
                                           END
                                ),
                                EditTime = GETDATE()
                            WHERE T_Project.Id = @ProjectId;";

                        var sql = $@";
                            IF EXISTS -- 身份验证：项目、审批类型、审批步骤、审批人符合要求，才会插入
                            (
                                SELECT p.Id
                                FROM dbo.T_Project p
                                    INNER JOIN dbo.T_FlowNode flow
                                        ON p.Id = @ProjectId
                                           AND p.FollowId = flow.Id
                                           AND flow.Type = @Type
                                           AND flow.FlowPositionId = @Step
                                           AND flow.IsDel = 0
                                    INNER JOIN dbo.T_ProjectUserFlowPosition position
                                        ON flow.FlowPositionId = position.FlowPositionId
                                           AND position.UserId = @userId
                                           AND position.ProjectId = @ProjectId
                                    INNER JOIN dbo.T_FlowPosition fp
                                        ON position.FlowPositionId = fp.Id
                            )
                            BEGIN
                                --插入审核记录
                                INSERT INTO dbo.T_ProjectFlowRecord
                                (
                                    ProjectId,
                                    UserId,
                                    UserName,
                                    Type,
                                    FlowNodeId,
                                    FlowNodeName,
                                    FlowPositionId,
                                    FlowPositionName,
                                    Result,
                                    Remark
                                )
                                SELECT @ProjectId,
                                       @userId,
                                       (
                                           SELECT Name FROM dbo.T_User WHERE Id = @userId
                                       ),
                                       @Type,
                                       flow.Id,
                                       flow.Name,
                                       flow.FlowPositionId,
                                       fp.Name,
                                       @result, 
                                       @Reason
                                FROM dbo.T_Project p
                                    INNER JOIN dbo.T_FlowNode flow
                                        ON p.Id = @ProjectId
                                           AND p.FollowId = flow.Id
                                           AND flow.Type = @Type
                                           AND flow.FlowPositionId = @Step
                                           AND flow.IsDel = 0
                                    INNER JOIN dbo.T_ProjectUserFlowPosition position
                                        ON flow.FlowPositionId = position.FlowPositionId
                                           AND position.UserId = @userId
                                           AND position.ProjectId = @ProjectId
                                    INNER JOIN dbo.T_FlowPosition fp
                                        ON position.FlowPositionId = fp.Id;
                                --维护项目的当前审批流节点：通过则继续下一节点，驳回则回到上一节点
                                {updateFollowIdSql};
                            END;";
                        var result = await conn.ExecuteAsync(sql, new
                        {
                            flowApprove.ProjectId,
                            userId = flowApprove.ApproveUserId,
                            flowApprove.Type,
                            flowApprove.Step,
                            flowApprove.Reason,
                            result = flowApprove.Approved
                        }, t) > 0;
                        if (result)
                        {
                            // 审批流状态回写:工长发包确认结果、客户确认结果
                            if ((flowApprove.Type == (int)ApproveEnum.FlowType.PackageConfirm && flowApprove.Step == (int)ApproveEnum.FlowStep.ConstructionMaster) ||
                                (flowApprove.Type == (int)ApproveEnum.FlowType.Disclosure && flowApprove.Step == (int)ApproveEnum.FlowStep.Customer))
                            {
                                int type;
                                if (flowApprove.Step == (int)ApproveEnum.FlowStep.ConstructionMaster)
                                {
                                    type = flowApprove.Approved == (int)ApproveEnum.ApproveResult.Pass ? (int)ApproveEnum.SubmitU9ApproveType.ConstructionMasterPass : (int)ApproveEnum.SubmitU9ApproveType.ConstructionMasterFail;
                                }
                                else
                                {
                                    type = flowApprove.Approved == (int)ApproveEnum.ApproveResult.Pass ? (int)ApproveEnum.SubmitU9ApproveType.CustomerPass : (int)ApproveEnum.SubmitU9ApproveType.CustomerFail;
                                }
                                var client = new JFClient();
                                var para = new ReOrder()
                                {
                                    Person = u9UserId,
                                    Type = type,
                                    Step = flowApprove.Step,
                                    SheedId = quotationId,
                                    DisReason = flowApprove.Reason
                                };
                                var response = await client.JFSendProStatusAsync(CommonConst.U9ServiceSource, auth.TimeSign, auth.Key, para);
                                var u9Result = JsonHelper.DeserializeJsonToObject<JFSendProStatus>(response);
                                if (u9Result.errcode == 0)
                                {
                                    t.Commit();
                                    return true;
                                }
                                Log4NetHelper.Error(response);
                                t.Rollback();
                            }
                            t.Commit();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        t.Rollback();
                        return false;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 获取项目的审批记录
        /// </summary>
        public async Task<List<ProjectFlowRecord>> GetProjectFlowRecord(int projectId)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var sql = @";
                    SELECT record.*,
                           node.PreNodeId
                    FROM dbo.T_Project p WITH (NOLOCK)
                        INNER JOIN dbo.T_ProjectFlowRecord record WITH (NOLOCK)
                            ON p.Id = record.ProjectId
                               AND p.Id = @projectId
                               AND p.IsDel = 0
                        INNER JOIN dbo.T_FlowNode node WITH (NOLOCK)
                            ON record.FlowNodeId = node.Id
                    ORDER BY record.Id;";
                return (await conn.QueryAsync<ProjectFlowRecord>(sql, new { projectId })).ToList();
            }
        }

        /// <summary>
        /// 获取项目的待审批记录
        /// </summary>
        public async Task<CurrentProjectFlow> GetProjectCurrentFlow(int projectId)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var sql = @";
                    SELECT node.Type,
                           node.Name AS NodeName,
                           node.FlowPositionId,
                           node.PreNodeId,
                           position.UserId,
                           position.UserName,
                           position.UserPhone,
                           fposition.Name AS PositionName
                    FROM dbo.T_Project p WITH (NOLOCK)
                        INNER JOIN dbo.T_FlowNode node WITH (NOLOCK)
                            ON p.FollowId = node.Id
                               AND p.Id = @projectId
                               AND node.IsDel = 0
                        INNER JOIN dbo.T_ProjectUserFlowPosition position WITH (NOLOCK)
                            ON node.FlowPositionId = position.FlowPositionId
                               AND position.ProjectId = @projectId
                        INNER JOIN dbo.T_FlowPosition fposition WITH (NOLOCK)
                            ON position.FlowPositionId = fposition.Id;";
                return await conn.QueryFirstOrDefaultAsync<CurrentProjectFlow>(sql, new { projectId });
            }
        }
    }
}
