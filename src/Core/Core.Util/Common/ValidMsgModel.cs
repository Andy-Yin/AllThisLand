namespace Core.Util.Common
{
    /// <summary>
    /// 验证公共消息类
    /// </summary>
    public class ValidMsgModel
    {
        /// <summary>
        /// 是否成功 true 成功 false 失败
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
    }
}
