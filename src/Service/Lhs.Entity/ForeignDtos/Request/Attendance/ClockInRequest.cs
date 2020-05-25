using System.ComponentModel.DataAnnotations;

namespace Lhs.Entity.ForeignDtos.Request.Attendance
{
    public class ClockInRequest
    {
        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }

        public int UserId { get; set; }

        public int ProjectId { get; set; }

        /// <summary>
        /// 类型：0 进场 1 离场
        /// </summary>
        public int Type { get; set; }

        [Required(ErrorMessage = "经度不能为空")]
        public string Longitude { get; set; }

        [Required(ErrorMessage = "纬度不能为空")]
        public string Latitude { get; set; }
    }
}
