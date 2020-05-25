using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Attendance;
using Lhs.Entity.ForeignDtos.Response.Attendance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lhs.Interface
{
    public interface IAttendanceRepository : IPlatformBaseService<T_PunchCardRecord>
    {
        /// <summary>
        /// 提交打卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> CheckIn(ClockInRequest request);

        /// <summary>
        /// 获取某个月份下有打卡记录的日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IEnumerable<MonthRecordsResponse>> MonthRecords(MonthRecordRequest request);
    }
}
