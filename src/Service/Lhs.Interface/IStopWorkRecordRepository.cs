using System;
using Core.Data;
using Core.Util.Common;
using Lhs.Entity.ForeignDtos.Response;
using Lhs.Entity.ForeignDtos.Response.User;
using Lhs.Entity.ForeignDtos.Response.UserRole;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Disclosure;
using Lhs.Entity.ForeignDtos.Response.Worker;

namespace Lhs.Interface
{
    public interface IStopWorkRecordRepository : IPlatformBaseService<T_StopWorkRecord>
    {
        Task<PageResponse<StopWorkRecordForAdmin>> GetStopWorkRecordList(
            EnumStopWorkType type,
            DateTime startFromDay, DateTime startToDay,
            DateTime stopFromDay, DateTime stopToDay,
            string search, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 删除停复工
        /// </summary>
        /// <param name="ids">要删除的id </param>
        Task<bool> DeleteChange(List<int> ids);
    }
}
