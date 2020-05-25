namespace Lhs.Entity.ForeignDtos.Request.User
{
    public class UserRoleInfo
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Memo { get; set; }
        
        public bool IsUsed { get; set; }
        
        public string BusinessPermissionString { get; set; }
        
        public string PermissionValueString { get; set; }
        public int UserRoleId { get; set; }
    }
}