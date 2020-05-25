namespace Core.Util
{
    public class SystemMonitorHelper
    {
        /// <summary>
        /// 将string类型的请求方式转换为数字
        /// </summary>
        /// <param name="method">string类型的请求方式</param>
        /// <returns>返回请求类型所对应的数字</returns>
        public static byte MethodStrConvertToByte(string method)
        {
            var result = (byte)0;
            switch (method)
            {
                case "GET":
                    result = (byte)HttpMethodEnum.Get;
                    break;
                case "POST":
                    result = (byte)HttpMethodEnum.Post;
                    break;
                case "PUT":
                    result = (byte)HttpMethodEnum.Put;
                    break;
                case "DELETE":
                    result = (byte)HttpMethodEnum.Delete;
                    break;
                case "HEAD":
                    result = (byte)HttpMethodEnum.Head;
                    break;
                case "OPTIONS":
                    result = (byte)HttpMethodEnum.Options;
                    break;
                case "TRACE":
                    result = (byte)HttpMethodEnum.Trace;
                    break;
                case "CONNECT":
                    result = (byte)HttpMethodEnum.Connect;
                    break;
                default:
                    return result;
            }
            return result;
        }

        /// <summary>
        /// 接口请求方式枚举
        /// 请求的方式(1:GET;2:POST;3:PUT;4:DELETE;5:HEAD;
        /// 6:OPTIONS;7:TRACE;8:CONNECT;)
        /// </summary>
        public enum HttpMethodEnum
        {
            /// <summary>
            /// GET
            /// </summary>
            Get = 1,

            /// <summary>
            /// POST
            /// </summary>
            Post = 2,

            /// <summary>
            /// PUT
            /// </summary>
            Put = 3,

            /// <summary>
            /// DELETE
            /// </summary>
            Delete = 4,

            /// <summary>
            /// HEAD
            /// </summary>
            Head = 5,

            /// <summary>
            /// OPTIONS
            /// </summary>
            Options = 6,

            /// <summary>
            /// TRACE
            /// </summary>
            Trace = 7,

            /// <summary>
            /// CONNECT
            /// </summary>
            Connect = 8,
        }
    }
}
