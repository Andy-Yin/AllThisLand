using System.Runtime.Serialization;

namespace Lhs.Common
{
    [DataContract]
    public class ResponseMessage
    {
        /// <summary>
        /// 默认构造函数（false）
        /// </summary>
        public ResponseMessage()
        {
            ErrCode = MessageResultCode.Error;
            ErrMsg = CommonMessage.GetFailure;
            Data = "";
        }

        /// <summary>
        /// 0 错误码
        /// </summary>
        [DataMember(Name = "errcode")]
        public MessageResultCode ErrCode { get; set; }

        /// <summary>
        /// 正确/错误信息
        /// </summary>
        [DataMember(Name = "errmsg")]
        public string ErrMsg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [DataMember(Name = "data")]
        public object Data { get; set; }
    }
}
