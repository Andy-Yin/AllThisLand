namespace Lhs.Entity.ForeignDtos.Response.PlatformMenu
{
    public class ButtonsData
    {
        public int ParentId { get; set; }

        public int ModuleId { get; set; }

        public int PermissionId { get; set; }

        public string Buttons => $"0-{ParentId}-{ModuleId}-{PermissionId}";
    }
}
