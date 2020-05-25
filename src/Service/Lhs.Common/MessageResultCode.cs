namespace Lhs.Common
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 失败
        /// </summary>
        Error = -1,

        /// <summary>
        /// 不正确的请求 400，用作拦截参数错误
        /// </summary>
        BadRequest = 400
    }
}
