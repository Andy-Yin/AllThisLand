namespace Lhs.Entity.ForeignDtos.Response
{
    /// <summary>
    /// 外部接口的返回信息基类
    /// </summary>
    public class BaseResponse<T> where T : new()
    {
        public BaseResponse()
        {
            Info = new T();
        }
        public bool Msg { get; set; }
        public string Message { get; set; }
        public int ResultCode { get; set; }
        public object Info { get; set; }
    }
}
