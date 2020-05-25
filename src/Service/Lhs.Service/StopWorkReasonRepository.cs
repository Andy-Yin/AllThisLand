using Core.Data;
using Core.Util.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Common;
using Lhs.Common.Const;
using Lhs.Entity.ForeignDtos.Request.User;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Lhs.Common.Enum;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Lhs.Service
{
    /// <summary>
    /// 工种
    /// </summary>
    public class StopWorkReasonRepository : PlatformBaseService<T_StopWorkReason>, IStopWorkReasonRepository
    {
        public StopWorkReasonRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取停复工原因
        /// </summary>
        public async Task<List<T_StopWorkReason>> GetStopWorkReasons()
        {
            using (var coon = Connection)
            {
                var sql = @"SELECT * FROM dbo.T_StopWorkReason WHERE IsDel=0";
                return (await coon.QueryAsync<T_StopWorkReason>(sql)).ToList();
            }
        }

        /// <summary>
        /// 删除停复工原因
        /// </summary>
        /// <param name="ids">要删除的id </param>
        public async Task<bool> DeleteWorkType(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_StopWorkReason SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }
    }
}
