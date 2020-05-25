namespace Core.Util.SettingModel
{
    /// <summary>
    /// jwt
    /// </summary>
    public class JwtSetting
    {
        /// <summary>
        /// 颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 可以使用的客户端
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string SecurityKey { get; set; }

        /// <summary>
        /// 过期时间-分钟
        /// </summary>
        public string ExpireMinutes { get; set; }
    }
}
