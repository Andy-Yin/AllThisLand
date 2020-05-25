namespace Lhs.Entity.ForeignDtos.Response.Task
{
    public class TaskListResponse
    {
        public int TaskId { get; set; } //AP32246165465165
        public string TaskNo { get; set; } //1
        public int MStatus { get; set; }
        public string Status { get {
                return MStatus switch
                {
                    1 => "待开工",
                    2 => "已开工",
                    3 => Type == 3 ? "已下单" : "待下单",
                    4 => "已取消",
                    5 => "已完成",
                    _ => "",
                };
            } } 
        public string TaskName { get; set; } //烟灶安装
        public int Type { get; set; } //1安装2测量3下单4派工
    }
}
