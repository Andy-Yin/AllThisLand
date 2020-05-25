namespace Lhs.Entity.ForeignDtos.Response
{
    public class DataBaseResponse
    {
        public DataBaseResponse()
        {
            Success = true;
            Message = string.Empty;
        }
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
