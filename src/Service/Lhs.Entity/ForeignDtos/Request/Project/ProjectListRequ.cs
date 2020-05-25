namespace Lhs.Entity.ForeignDtos.Request.Project
{
    public class ProjectListRequ : ReqAuth
    {
        public int Status { get; set; }

        public int PackageStatus { get; set; }

        public string Search { get; set; }

        public string CompanyId { get; set; }

        public string sStartTime { get; set; }

        public string sEndTime { get; set; }

        public string eStartTime { get; set; }

        public string eEndTime { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

    }
}
