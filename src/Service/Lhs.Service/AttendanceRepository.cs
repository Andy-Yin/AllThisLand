using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Request.Attendance;
using Lhs.Entity.ForeignDtos.Response.Attendance;
using Lhs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Lhs.Service
{
    public class AttendanceRepository : PlatformBaseService<T_PunchCardRecord>, IAttendanceRepository
    {
        public AttendanceRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CheckIn(ClockInRequest request)
        {
            var punch = new T_PunchCardRecord
            {
                ProjectId = request.ProjectId,
                Type = (byte)request.Type,
                UserId = request.UserId,
                Location = request.Address,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                IsDel = false,
                CreateTime = DateTime.Now
            };
            return await this.AddAsync(punch) > 0;
        }

        /// <summary>
        /// 获取某个年月下有打卡记录的日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MonthRecordsResponse>> MonthRecords(MonthRecordRequest request)
        {
            var sql = $@"
                SELECT
                    convert(varchar(100), punch.CreateTime,20) AS CheckInTime, --2020-01-01 11:00:00
					convert(varchar(7), punch.CreateTime,120) AS [Date],
					mUser.[Name],
                    Type,
					mUser.Phone
				FROM
					T_PunchCardRecord punch
					LEFT JOIN T_User mUser ON mUser.Id = punch.UserId
				WHERE
					punch.UserId = {request.UserId} 
					AND punch.ProjectId = {request.ProjectId}
					AND mUser.IsDel = 0
					AND punch.IsDel = 0
					AND SUBSTRING (CONVERT(VARCHAR(10), punch.CreateTime, 120 ), 0, 8) = '{request.Month}'";
            var data = await this.FindAllAsync<PunchRecordForDataBase>(sql) as List<PunchRecordForDataBase>;
            var result = new List<MonthRecordsResponse>();

            if (data.Any())
            {
                // 按照日期分组
                var groupDate = data.GroupBy(m => m.Date).ToList();
                foreach (var item in groupDate)
                {
                    var model = new MonthRecordsResponse
                    {
                        Date = item.Key.ToString("yyyy-MM-dd"),
                        Records = item.Select(n => new PunchRecord
                        {
                            Name = n.Name,
                            Phone = n.Phone,
                            Type = n.Type,
                            CheckInTime = n.CheckInTime

                        }).ToList()
                    };
                    result.Add(model);
                }
            }

            return result;
        }
    }
}
