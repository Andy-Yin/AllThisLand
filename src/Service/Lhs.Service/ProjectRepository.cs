using System;
using Core.Data;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Project;
using Lhs.Entity.ForeignDtos.Response.Project;
using Lhs.Interface;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lhs.Common.Const;
using Lhs.Common.Enum;
using Lhs.Entity.ForeignDtos.Response.Construction;

namespace Lhs.Service
{
    public class ProjectRepository : PlatformBaseService<T_Project>, IProjectRepository
    {

        public ProjectRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 报价单是否存在
        /// </summary>
        /// <param name="quotationId"></param>
        public async Task<bool> IsPackageExist(string quotationId)
        {
            using (var conn = Connection)
            {
                var sql = @";SELECT COUNT(1) FROM dbo.T_Project WHERE QuotationId=@quotationId AND IsDel=0";
                return await conn.ExecuteScalarAsync<int>(sql, new { quotationId }) > 0;
            }
        }

        /// <summary>
        /// 审批流中的用户不存在的岗位
        /// </summary>
        public async Task<int> IsFlowPositionExist(string constructionMasterId, string solidDesignerId, string softDesignerId, string supervisionId)
        {
            using (var conn = Connection)
            {
                var sql = @";
                    SELECT CASE
                               WHEN NOT EXISTS
                                        (
                                            (SELECT u.Id
                                             FROM dbo.T_User u WITH (NOLOCK)
                                             WHERE u.U9UserId = @constructionMasterId)
                                        ) THEN
                                   1
                               WHEN NOT EXISTS
                                        (
                                            SELECT u.Id
                                            FROM dbo.T_User u WITH (NOLOCK)
                                            WHERE u.U9UserId = @solidDesignerId
                                        ) THEN
                                   2
                               WHEN NOT EXISTS
                                        (
                                            SELECT u.Id
                                            FROM dbo.T_User u WITH (NOLOCK)
                                            WHERE u.U9UserId = @softDesignerId
                                        ) THEN
                                   5
                               WHEN NOT EXISTS
                                        (
                                            SELECT u.Id
                                            FROM dbo.T_User u WITH (NOLOCK)
                                            WHERE u.U9UserId = @supervisionId
                                        ) THEN
                                   4
                               ELSE
                                   0
                           END;";
                return await conn.ExecuteScalarAsync<int>(sql, new { constructionMasterId, solidDesignerId, softDesignerId, supervisionId });
            }
        }

        /// <summary>
        /// U9添加项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddProject(AddProjectReq request)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                using (var t = conn.BeginTransaction())
                {
                    try
                    {
                        var project = new T_Project()
                        {
                            ProjectNo = request.ProjectNo,
                            ContractNo = request.ContractNo,
                            CustomerNo = request.CustomerNo,
                            QuotationId = request.QuotationId,
                            ProjectName = request.ProjectName,
                            Address = request.Address,
                            DecorateType = request.DecorateType,
                            PlanStartDate = Convert.ToDateTime(request.PlanStartDate).Date,
                            PlanEndDate = Convert.ToDateTime(request.PlanEndDate).Date,
                            Area = request.Area,
                            Status = (int)ProjectEnum.ProjectStatus.WaitStart,
                            FollowId = ProjectConst.ProjectFlowStartId,
                            CompanyId = request.CompanyId,
                            CustomerName = request.CustomerName,
                            CustomerPhone = request.CustomerPhone,
                            DeliverDepartmentId = request.DeliverDepartmentId,
                            DeliverManagerId = request.DeliverManagerId,
                            SolidDesignerId = request.SolidDesignerId,
                            SoftDesignerId = request.SoftDesignerId,
                            ConstructionMasterId = request.ConstructionMasterId,
                            SupervisionId = request.SupervisionId,
                            ProjectAssistantId = request.ProjectAssistantId,
                            ProjectMinisterId = request.ProjectMinisterId,
                            SolidDesignerManagerId = request.SolidDesignerManagerId,
                            SoftDesignerManagerId = request.SoftDesignerManagerId,
                        };
                        await AddAsync(conn, project, t);
                        if (project.Id > 0)
                        {
                            var sql = @";
                                -- 插入审批流需要的项目岗位信息
                                INSERT INTO dbo.T_ProjectUserFlowPosition
                                (
                                    ProjectId,
                                    FlowPositionId,
                                    UserId,
                                    UserName,
                                    UserPhone
                                )
                                SELECT *
                                FROM
                                (
                                    SELECT p.Id AS ProjectId,
                                           f.Id AS FlowPositionId,
                                           CASE f.Id
                                               WHEN 1 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.ConstructionMasterId
                                               )
                                               WHEN 2 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.SolidDesignerId
                                               )
                                               WHEN 3 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.SolidDesignerManagerId
                                               )
                                               WHEN 4 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.SupervisionId
                                               )
                                               WHEN 5 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.SoftDesignerId
                                               )
                                               WHEN 6 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.SoftDesignerManagerId
                                               )
                                               WHEN 7 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.ProjectAssistantId
                                               )
                                               WHEN 8 THEN
                                               (
                                                   SELECT u.Id FROM dbo.T_User u WHERE U9UserId = p.ProjectMinisterId
                                               )
                                               WHEN 9 THEN
                                           (0)
                                           END AS UserId,
                                           CASE f.Id
                                               WHEN 1 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.ConstructionMasterId
                                               )
                                               WHEN 2 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.SolidDesignerId
                                               )
                                               WHEN 3 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.SolidDesignerManagerId
                                               )
                                               WHEN 4 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.SupervisionId
                                               )
                                               WHEN 5 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.SoftDesignerId
                                               )
                                               WHEN 6 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.SoftDesignerManagerId
                                               )
                                               WHEN 7 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.ProjectAssistantId
                                               )
                                               WHEN 8 THEN
                                               (
                                                   SELECT u.Name FROM dbo.T_User u WHERE U9UserId = p.ProjectMinisterId
                                               )
                                               WHEN 9 THEN
                                           (p.CustomerName)
                                           END AS UserName,
                                           CASE f.Id
                                               WHEN 1 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.ConstructionMasterId
                                               )
                                               WHEN 2 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.SolidDesignerId
                                               )
                                               WHEN 3 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.SolidDesignerManagerId
                                               )
                                               WHEN 4 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.SupervisionId
                                               )
                                               WHEN 5 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.SoftDesignerId
                                               )
                                               WHEN 6 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.SoftDesignerManagerId
                                               )
                                               WHEN 7 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.ProjectAssistantId
                                               )
                                               WHEN 8 THEN
                                               (
                                                   SELECT u.Phone FROM dbo.T_User u WHERE U9UserId = p.ProjectMinisterId
                                               )
                                               WHEN 9 THEN
                                           (p.CustomerPhone)
                                           END AS UserPhone
                                    FROM dbo.T_FlowPosition f WITH (NOLOCK)
                                        LEFT JOIN dbo.T_Project p WITH (NOLOCK)
                                            ON p.Id = @projectId
                                ) a
                                WHERE a.UserId IS NOT NULL;
                                -- 插入一条U9发包的审批记录
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
                                VALUES
                                (   @projectId, -- ProjectId - int
                                    0,          -- UserId - int
                                    N'',        -- UserName - nvarchar(100)
                                    1,          -- Type - int
                                    1,          -- FlowNodeId - int
                                    (
                                        SELECT Name FROM dbo.T_FlowNode WHERE Id = 1
                                    ),          -- FlowNodeName - nvarchar(100)
                                    0,          -- FlowPositionId - int
                                    N'U9系统发包',  -- FlowPositionName - nvarchar(100)
                                    1,          -- Result - int
                                    (
                                        SELECT TOP 1
                                               CONCAT('包已发送到工长端 ', Name)
                                        FROM dbo.T_User
                                        WHERE U9UserId = @ConstructionMasterId
                                    )           -- Remark - nvarchar(200)
                                    );
                                -- 插入客户
                                IF NOT EXISTS
                                (
                                    SELECT c.Id
                                    FROM dbo.T_Customer c WITH (NOLOCK)
                                    WHERE c.Phone = @CustomerPhone
                                          AND c.IsDel = 0
                                )
                                    INSERT INTO dbo.T_Customer
                                    (
                                        Phone,
                                        Name,
                                        Password
                                    )
                                    VALUES
                                    (   @CustomerPhone,  -- Phone - varchar(11)
                                        @CustomerName, -- Name - nvarchar(100)
                                        'e10adc3949ba59abbe56e057f20f883e');";

                            var result = conn.Execute(sql, new
                            {
                                projectId = project.Id,
                                request.ConstructionMasterId,
                                request.CustomerPhone,
                                request.CustomerName
                            }, t) > 0;
                            if (result)
                            {
                                // 提交数据
                                t.Commit();
                                return true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        t.Rollback();
                        return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取施工计划
        /// </summary>
        /// <param name="no"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<PlanProjectList>> GetPlanProjectList(string no, int id)
        {
            // 项目计划
            var projectPlan = await this.ExecuteScalarAsync<T_ProjectConstructionPlan>($" WHERE ProjectId={id} AND IsDel = 0") as T_ProjectConstructionPlan;
            // 项目主材地材项目
            var pMaterialItem = await this.FindAllAsync<T_ProjectMaterialItem>($" WHERE ProjectId={id} AND IsDel = 0") as List<T_ProjectMaterialItem>;
            // 计划模板
            var pPlanTemp = await this.FindAllAsync<T_ConstructionPlanTemplate>($" WHERE ProjectId={id} AND IsDel = 0") as List<T_ConstructionPlanTemplate>;
            var tempIds = pPlanTemp.Select(m => m.Id).ToList();
            // 计划项目项目 关联模板id
            var planItem = await this.FindAllAsync<T_ConstructionPlanItem>($" WHERE TemplateId IN ({string.Join(',', tempIds)}) AND IsDel = 0") as List<T_ConstructionPlanItem>;

            throw new System.NotImplementedException();
        }

        public async Task<List<ProjectUser>> GetProjectUserListByProjectId(int projectId)
        {
            using (var coon = Connection)
            {
                string sql = @$"
select p.id,
       pufp.FlowPositionId PositionId,
       p.ProjectNo,
       pufp.UserId,
       pufp.UserName,
       pufp.UserPhone,
       pufp.UserId,
       fp.Name RoleName
from T_Project p
         left join T_ProjectUserFlowPosition pufp on p.Id = pufp.ProjectId
         left join T_FlowPosition fp on fp.Id = pufp.FlowPositionId
where p.Id = {projectId} and p.IsDel = 0";
                return (await coon.QueryAsync<ProjectUser>(sql)).ToList();
            }

        }

        public async Task<T_Project> GetProjectByQuotationId(string quotationId)
        {
            string sql = @$"select * from T_Project where QuotationId = '{quotationId}' and isdel = 0";
            var list = (await this.AllAsync(sql)).ToList();
            if (!list.Any())
            {
                return null;
            }

            return list.First();
        }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PageResponse<ProjectListResp>> GetProjectList(ProjectListRequ request)
        {
            var query = $" AND 1=1 "; ;
            if (request.Status > 0)
            {
                query += $" AND p.Status = {request.Status}";
            }
            if (!string.IsNullOrEmpty(request.Search))
            {
                query += $" AND (p.ProjectNo LIKE '%{request.Search}%' OR p.ProjectNo LIKE '%{request.Search}%' OR p.CustomerName LIKE '%{request.Search}%')";
            }
            if (!string.IsNullOrEmpty(request.sStartTime))
            {
                query += $" AND p.PlanStartDate <= '{request.sStartTime}'";
            }
            if (!string.IsNullOrEmpty(request.sEndTime))
            {
                query += $" AND p.PlanEndDate >= '{request.sEndTime}'";
            }
            if (!string.IsNullOrEmpty(request.eStartTime))
            {
                query += $" AND p.ActualStartDate >= '{request.eStartTime}'";
            }
            if (!string.IsNullOrEmpty(request.eEndTime))
            {
                query += $" AND p.ActualEndDate >= '{request.eEndTime}'";
            }
            if (!string.IsNullOrEmpty(request.CompanyId))
            {
                query += $" AND p.CompanyId = @CompanyId";
            }
            if (request.PackageStatus > 0)
            {
                switch (request.PackageStatus)
                {
                    case (int)ProjectEnum.ProjectPackageStatus.NewPackage:
                        query += " AND p.FollowId = 2";
                        break;
                    case (int)ProjectEnum.ProjectPackageStatus.OnGoing:
                        query += " AND p.FollowId > 2";
                        break;
                    case (int)ProjectEnum.ProjectPackageStatus.Finished:
                        query += " AND p.FollowId = 0";
                        break;
                    case (int)ProjectEnum.ProjectPackageStatus.Canceled:
                        query += " AND p.FollowId = 1";
                        break;
                }
            }

            var sql = $@"
                SELECT p.Id,
                       p.ProjectNo,
                       p.CustomerNo,
                       p.ProjectName,
                       p.CustomerPhone,
                       p.CustomerName,
                       p.QuotationId,
                       (
                           SELECT Name FROM dbo.T_Company WHERE CompanyId = p.CompanyId AND IsDel = 0
                       ) AS CompanyName,
					CONVERT ( VARCHAR ( 20 ), p.PlanStartDate, 20 ) AS PlanStartTime,
					CONVERT ( VARCHAR ( 20 ), p.PlanEndDate, 20 ) AS PlanFinishTime,
					CONVERT ( VARCHAR ( 20 ), p.ActualStartDate, 23 ) AS RealStartTime,
					CONVERT ( VARCHAR ( 20 ), p.ActualEndDate, 23 ) AS RealFinishTime,
                       p.[Status],
                       p.DecorateType AS DecorateType,
                       (
                           SELECT Name
                           FROM dbo.T_Department
                           WHERE DepartmentId = p.DeliverDepartmentId
                                 AND IsDel = 0
                       ) AS DeliverDepartment,
                       (
                           SELECT Name
                           FROM dbo.T_User
                           WHERE U9UserId = p.DeliverManagerId
                                 AND IsDel = 0
                       ) AS DeliverManager,
                       (
                           SELECT Name FROM dbo.T_User WHERE U9UserId = p.SupervisionId AND IsDel = 0
                       ) AS Supervisor,
                       (
                           SELECT Name
                           FROM dbo.T_User
                           WHERE U9UserId = p.SolidDesignerId
                                 AND IsDel = 0
                       ) AS Designer,
                       CASE
                           WHEN p.FollowId = 2 THEN -- 新接入
                               1
                           WHEN p.FollowId > 2 THEN -- 流转中
                               2
                           WHEN p.FollowId = 0 THEN -- 已完成
                               3
                           WHEN p.FollowId = 1 THEN -- 回收站
                               4
                       END AS PackageStatus
                FROM T_Project p
                WHERE p.IsDel = 0
                    {query}";
            return await this.PagedAsync<ProjectListResp>(sql, new { request.CompanyId }, "Id DESC", request.PageIndex, request.PageSize);
        }

        /// <summary>
        /// 获取客户的项目列表
        /// </summary>
        public async Task<List<ProjectListResp>> GetCustomerProjectList(int userId, string name)
        {
            using (var coon = Connection)
            {
                var query = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    query += " AND p.ProjectName LIKE @name";
                }

                var sql = $@"
                    SELECT p.Id,
                           p.ProjectNo,
                           p.CustomerNo,
                           p.ProjectName,
                           p.CustomerPhone,
                           p.CustomerName,
                           p.QuotationId,
                           (
                               SELECT Name FROM dbo.T_Company WHERE CompanyId = p.CompanyId AND IsDel = 0
                           ) AS CompanyName,
                           CONVERT(VARCHAR(20), p.PlanStartDate, 20) AS PlanStartTime,
                           CONVERT(VARCHAR(20), p.PlanEndDate, 20) AS PlanFinishTime,
                           CONVERT(VARCHAR(20), p.ActualStartDate, 23) AS RealStartTime,
                           CONVERT(VARCHAR(20), p.ActualEndDate, 23) AS RealFinishTime,
                           p.[Status],
                           p.DecorateType AS DecorateType,
                           (
                               SELECT Name
                               FROM dbo.T_Department
                               WHERE DepartmentId = p.DeliverDepartmentId
                                     AND IsDel = 0
                           ) AS DeliverDepartment,
                           (
                               SELECT Name
                               FROM dbo.T_User
                               WHERE U9UserId = p.DeliverManagerId
                                     AND IsDel = 0
                           ) AS DeliverManager,
                           (
                               SELECT Name FROM dbo.T_User WHERE U9UserId = p.SupervisionId AND IsDel = 0
                           ) AS Supervisor,
                           (
                               SELECT Name
                               FROM dbo.T_User
                               WHERE U9UserId = p.SolidDesignerId
                                     AND IsDel = 0
                           ) AS Designer
                    FROM dbo.T_Project p WITH (NOLOCK)
                        INNER JOIN dbo.T_Customer c WITH (NOLOCK)
                            ON p.CustomerPhone = c.Phone
                               AND p.IsDel = 0
                               AND c.Id = @userId
                               AND c.IsDel = 0
                               {query}";
                return (await coon.QueryAsync<ProjectListResp>(sql, new { name = $"%{name}%", userId })).ToList();
            }
        }

        /// <summary>
        /// 获取用户的项目列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="position">类型：1 工长 4 监理</param>
        /// <param name="name">搜索条件</param>
        /// <param name="pageNum">页码</param>
        /// <param name="status">项目状态：1 待开工 2 准备期 3 在建 4 已竣工 5 已停工</param>
        public async Task<PageResponse<UserProjectInfo>> GetUserProjectList(int userId, int position, string name, int pageNum, int status)
        {
            var where = string.Empty;
            if (!string.IsNullOrEmpty(name))
            {
                where += @" AND p.ProjectName LIKE @name";
            }
            if (status > 0)
            {
                where += @" AND p.Status = @status";
            }
            var sql = $@"
                    SELECT p.*,
                           (
                               SELECT Name
                               FROM dbo.T_User WITH (NOLOCK)
                               WHERE U9UserId = p.ConstructionMasterId
                                     AND IsDel = 0
                           ) AS ConstructionMasterName,
                           (
                               SELECT Phone
                               FROM dbo.T_User WITH (NOLOCK)
                               WHERE U9UserId = p.ConstructionMasterId
                                     AND IsDel = 0
                           ) AS ConstructionMasterPhone,
                           (
                               SELECT Name
                               FROM dbo.T_User WITH (NOLOCK)
                               WHERE U9UserId = p.SupervisionId
                                     AND IsDel = 0
                           ) AS SupervisorName,
                           (
                               SELECT Phone
                               FROM dbo.T_User WITH (NOLOCK)
                               WHERE U9UserId = p.SupervisionId
                                     AND IsDel = 0
                           ) AS SupervisorPhone,
                           (
                               SELECT Name
                               FROM dbo.T_User WITH (NOLOCK)
                               WHERE U9UserId = p.SolidDesignerId
                                     AND IsDel = 0
                           ) AS DesignerName,
                           (
                               SELECT Phone
                               FROM dbo.T_User WITH (NOLOCK)
                               WHERE U9UserId = p.SolidDesignerId
                                     AND IsDel = 0
                           ) AS DesignerPhone
                    FROM dbo.T_ProjectUserFlowPosition pufp WITH (NOLOCK)
                        INNER JOIN dbo.T_Project p WITH (NOLOCK)
                            ON pufp.ProjectId = p.Id
                               AND pufp.UserId = @userId
                               AND pufp.FlowPositionId = @position
                               AND p.IsDel = 0
                               {where}";
            return await this.PagedAsync<UserProjectInfo>(sql, new
            {
                userId,
                name = $"%{name}%",
                status,
                position
            }, "", pageNum, CommonConst.PageSize);
        }

        /// <summary>
        /// 保存项目的预交底 交底模板
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="type">1 预交底 2 交底</param>
        /// <param name="templateId"></param>
        public async Task<bool> SaveProjectDisclosure(int projectId, int type, int templateId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                INSERT INTO dbo.T_ProjectDisclosure
                (
                    ProjectId,
                    TemplateId,
                    Type,
                    DisclosureItemId,
                    DisclosureItemName,
                    Remark
                )
                SELECT @projectId,
                       @templateId,
                       @type,
                       item.Id,
                       item.Name,
                       item.Remark
                FROM dbo.T_DisclosureTemplateItem titem WITH (NOLOCK)
                    INNER JOIN dbo.T_DisclosureItem item WITH (NOLOCK)
                        ON titem.TemplateId = @templateId
                           AND titem.IsDel = 0
                           AND titem.ItemId = item.Id
                           AND item.IsDel = 0;";
                return await coon.ExecuteAsync(sql, new { projectId, type = type - 1, templateId }) > 0;
            }
        }

        /// <summary>
        /// 保存项目的施工管理模板
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="templateId"></param>
        public async Task<bool> SaveProjectConstructionManage(int projectId, int templateId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    DECLARE @@category TABLE
                    (
                        pCategoryId INT,
                        categoryId INT
                    );
                    DECLARE @@task TABLE
                    (
                        pTaskId INT,
                        taskId INT
                    );
                    --插入施工管理工种
                    INSERT INTO dbo.T_ProjectConstructionManage
                    (
                        ProjectId,
                        TemplateId,
                        CategoryId,
                        CategoryName
                    )
                    OUTPUT Inserted.Id,
                           Inserted.CategoryId
                    INTO @@category
                    SELECT @projectId,
                           @templateId,
                           category.Id,
                           category.Name
                    FROM dbo.T_ConstructionManageTemplateItem titem WITH (NOLOCK)
                        INNER JOIN dbo.T_ConstructionManageCategory category WITH (NOLOCK)
                            ON titem.TemplateId = @templateId
                               AND titem.IsDel = 0
                               AND category.IsDel = 0
                               AND titem.CategoryId = category.Id;
                    --插入任务
                    INSERT INTO dbo.T_ProjectConstructionCheckTask
                    (
                        ProjectManageId,
                        TaskNo,
                        ManageTaskId,
                        ManageTaskName
                    )
                    OUTPUT Inserted.Id,
                           Inserted.ManageTaskId
                    INTO @@task
                    SELECT category.pCategoryId,
                           '',
                           task.Id,
                           task.Name
                    FROM @@category category
                        INNER JOIN dbo.T_ConstructionManageCheckTask task WITH (NOLOCK)
                            ON category.categoryId = task.CategoryId
                               AND task.IsDel = 0;
                    --插入验收标准
                    INSERT INTO dbo.T_ProjectConstructionCheckStandard
                    (
                        ProjectTaskId,
                        StandardId,
                        Name,
                        Content
                    )
                    SELECT task.pTaskId,
                           stand.Id,
                           stand.Name,
                           stand.Content
                    FROM @@task task
                        INNER JOIN dbo.T_ConstructionManageCheckStandard stand WITH (NOLOCK)
                            ON stand.IsDel = 0
                               AND stand.TaskId = task.taskId;";
                return await coon.ExecuteAsync(sql, new { projectId, templateId }) > 0;
            }
        }

        /// <summary>
        /// 保存项目的施工计划模板
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="templateId"></param>
        public async Task<bool> SaveProjectConstructionPlan(int projectId, int templateId)
        {
            using (var coon = Connection)
            {
                var sql = @";
                    DECLARE @@stage TABLE
                    (
                        pStageId INT,
                        stageId INT
                    );
                    --插入阶段
                    INSERT INTO dbo.T_ProjectPlanStage
                    (
                        ProjectId,
                        StageId,
                        Name,
                        Days
                    )
                    OUTPUT Inserted.Id,
                           Inserted.StageId
                    SELECT @projectId,
                           stage.Id,
                           stage.Name,
                           stage.Days
                    FROM dbo.T_ConstructionPlanStage stage WITH (NOLOCK)
                    WHERE stage.IsDel = 0;
                    --插入施工计划
                    INSERT INTO dbo.T_ProjectConstructionPlan
                    (
                        ProjectId,
                        TemplateId,
                        PlanItemId,
                        Name,
                        Days,
                        ProjectStageId,
                        ContractDays,
                        Contents
                    )
                    SELECT @projectId,
                           @templateId,
                           item.Id,
                           item.Name,
                           item.InnerDays,
                           CASE
                               WHEN EXISTS
                                    (
                                        SELECT pStageId FROM @@stage WHERE stageId = item.StageId
                                    ) THEN
                               (
                                   SELECT TOP 1 pStageId FROM @@stage WHERE stageId = item.StageId
                               )
                               ELSE
                                   0
                           END,
                           item.ContractDays,
                           item.Contents
                    FROM dbo.T_ConstructionPlanTemplateItem pitem WITH (NOLOCK)
                        INNER JOIN dbo.T_ConstructionPlanItem item WITH (NOLOCK)
                            ON pitem.IsDel = 0
                               AND pitem.TemplateId = @templateId
                               AND pitem.CategoryId = item.Id
                               AND item.IsDel = 0;";
                return await coon.ExecuteAsync(sql, new { projectId, templateId }) > 0;
            }
        }

        /// <summary>
        /// 获取项目匹配的模板
        /// </summary>
        public async Task<List<MatchingTemplate>> GetMatchingTemplate(int projectId)
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var sql = @";
                    SELECT a.Type,
                           a.TemplateId,
                           (
                               SELECT Name FROM dbo.T_DisclosureTemplate WHERE Id = a.TemplateId
                           ) AS TemplateName
                    FROM
                    (
                        SELECT 1 AS Type,
                               ISNULL(MAX(dis.TemplateId), 0) AS TemplateId
                        FROM dbo.T_Project p WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectDisclosure dis WITH (NOLOCK)
                                ON p.Id = dis.ProjectId
                                   AND dis.Type = 0
                                   AND dis.TemplateId > 0
                        WHERE p.Id = @projectId
                        GROUP BY p.Id
                    ) a
                    UNION
                    SELECT a.Type,
                           a.TemplateId,
                           (
                               SELECT Name FROM dbo.T_DisclosureTemplate WHERE Id = a.TemplateId
                           ) AS TemplateName
                    FROM
                    (
                        SELECT 2 AS Type,
                               ISNULL(MAX(dis.TemplateId), 0) AS TemplateId
                        FROM dbo.T_Project p WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectDisclosure dis WITH (NOLOCK)
                                ON p.Id = dis.ProjectId
                                   AND dis.Type = 1
                                   AND dis.TemplateId > 0
                        WHERE p.Id = @projectId
                        GROUP BY p.Id
                    ) a
                    UNION
                    SELECT a.Type,
                           a.TemplateId,
                           (
                               SELECT Name FROM dbo.T_ConstructionManageTemplate WHERE Id = a.TemplateId
                           ) AS TemplateName
                    FROM
                    (
                        SELECT 3 AS Type,
                               ISNULL(MAX(dis.TemplateId), 0) AS TemplateId
                        FROM dbo.T_Project p WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectConstructionManage dis WITH (NOLOCK)
                                ON p.Id = dis.ProjectId
                                   AND dis.TemplateId > 0
                        WHERE p.Id = @projectId
                        GROUP BY p.Id
                    ) a
                    UNION
                    SELECT a.Type,
                           a.TemplateId,
                           (
                               SELECT Name FROM dbo.T_ConstructionPlanTemplate WHERE Id = a.TemplateId
                           ) AS TemplateName
                    FROM
                    (
                        SELECT 4 AS Type,
                               ISNULL(MAX(dis.TemplateId), 0) AS TemplateId
                        FROM dbo.T_Project p WITH (NOLOCK)
                            LEFT JOIN dbo.T_ProjectConstructionPlan dis WITH (NOLOCK)
                                ON p.Id = dis.ProjectId
                                   AND dis.TemplateId > 0
                        WHERE p.Id = @projectId
                        GROUP BY p.Id
                    ) a;";
                return (await conn.QueryAsync<MatchingTemplate>(sql, new { projectId })).ToList();
            }
        }

        /// <summary>
        /// 获取所有项目
        /// </summary>
        public async Task<List<T_Project>> GetAllProject()
        {
            using (var conn = Connection)
            {
                await conn.OpenAsync();
                var sql = @";
                    SELECT *
                    FROM dbo.T_Project WITH(NOLOCK)
                    WHERE IsDel = 0
                    ORDER BY Id;";
                return (await conn.QueryAsync<T_Project>(sql)).ToList();
            }
        }

    }
}
