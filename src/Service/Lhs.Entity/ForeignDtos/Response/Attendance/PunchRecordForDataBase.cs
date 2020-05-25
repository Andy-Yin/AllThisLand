using System;

namespace Lhs.Entity.ForeignDtos.Response.Attendance
{
    public class PunchRecordForDataBase
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        // 0-进，1-出
        public int Type { get; set; }

        public string CheckInTime { get; set; }
    }
}
