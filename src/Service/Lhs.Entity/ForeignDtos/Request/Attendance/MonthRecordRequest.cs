using System.ComponentModel.DataAnnotations;

namespace Lhs.Entity.ForeignDtos.Request.Attendance
{
    public class MonthRecordRequest : ReqAuth
    {
        [Required(ErrorMessage = "月份不能为空")] public string Month { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }
    }
}
