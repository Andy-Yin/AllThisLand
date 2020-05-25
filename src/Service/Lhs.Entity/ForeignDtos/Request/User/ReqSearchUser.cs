namespace Lhs.Entity.ForeignDtos.Response.User
{
    public class ReqSearchUser
    {
        public string Name { get; set; }
        
        public string Phone { get; set; }
        
        public int PageIndex { get; set; }
        
        public int PageSize { get; set; }

    }
}