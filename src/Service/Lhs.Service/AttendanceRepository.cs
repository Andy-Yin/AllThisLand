using Lhs.Entity.DbEntity.DbModel;
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
    }
}
