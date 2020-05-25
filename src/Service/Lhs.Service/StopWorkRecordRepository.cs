using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Data;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 停复工
    /// </summary>
    public class StopWorkRecordRepository : PlatformBaseService<T_StopWorkRecord>, IStopWorkRecordRepository
    {
        public StopWorkRecordRepository(IConfiguration config)
        {
            Config = config;
        }

        public async Task<PageResponse<StopWorkRecordForAdmin>> GetStopWorkRecordList(
            EnumStopWorkType type, DateTime startFromDay,
            DateTime startToDay, DateTime stopFromDay, DateTime stopToDay, string search,
            int pageIndex = 1, int pageSize = 20)
        {
            using (var coon = Connection)
            {
                var startDate = string.Empty;
                var endDate = string.Empty;
                if (type == EnumStopWorkType.Start)
                {
                    startDate = startFromDay.Date.ToString("yyyy-MM-dd");
                    endDate = startToDay.Date.ToString("yyyy-MM-dd");
                }
                else
                {
                    startDate = stopFromDay.Date.ToString("yyyy-MM-dd");
                    endDate = stopToDay.Date.ToString("yyyy-MM-dd");
                }

                string startSql = string.Empty;
                if (startDate != "0001-01-01")
                {
                    startSql = @$"and PlanDate >= '{startDate}'";
                }

                string endSql = string.Empty;
                if (startDate != "0001-01-01")
                {
                    endSql = @$" and PlanDate <= '{endDate}'";
                }

                string sql = @$"
select * from (
                  select swr.*,
                         p.ProjectName,
                         p.ProjectNo,
                         (select pufp.UserName
                          from T_ProjectUserFlowPosition pufp
                          where pufp.ProjectId = p.Id and pufp.FlowPositionId = 1) ConstructionManagerName,
                         (select pufp.UserName
                          from T_ProjectUserFlowPosition pufp
                          where pufp.ProjectId = p.Id and pufp.FlowPositionId = 4) SupervisorName
                  from T_StopWorkRecord swr
                           left join t_project p on swr.ProjectId = p.Id
                  where swr.IsDel = 0
                    and p.IsDel = 0
                    and swr.Type = {(int) type}
                    {startSql}  
                    {endSql} 
              ) a
where 1=1 ";

                
                if (!string.IsNullOrEmpty(search))
                {
                    sql += @$" and ProjectName like '%{search}%' or ProjectNo like '%{search}%' or ConstructionManagerName like '%{search}%'";
                }

                var result = await this.PagedAsync<StopWorkRecordForAdmin>(sql, null, "Id", pageIndex, pageSize);

                return result;
            }
        }

        public async Task<bool> DeleteChange(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_StopWorkRecord SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
