using System.Collections.Generic;

namespace Lhs.Entity.ForeignDtos.Response.Attendance
{
    public class MonthRecordsResponse
    {
        public string Date { get; set; }

        public List<PunchRecord> Records { get; set; }
    }

    public class PunchRecord {
        public string Name { get; set; }

        public string Phone { get; set; }

        // 0-进，1-出
        public int Type { get; set; }

        public string CheckInTime { get; set; }

    }
}
