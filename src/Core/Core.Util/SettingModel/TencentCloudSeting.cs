namespace Core.Util.SettingModel
{
    /// <summary>
    /// 腾讯云的配置
    /// </summary>
    public class TencentCloudSeting
    {
        /// <summary>
        /// 云验证码的安全id
        /// </summary>
        public string VCodeSecretId { get; set; }

        /// <summary>
        /// 云验证码的安全key
        /// </summary>
        public string VCodeSecretKey { get; set; }

        /// <summary>
        /// 验证码的appId
        /// </summary>
        public ulong CaptchaAppId { get; set; }

        /// <summary>
        /// AppSecretKey
        /// </summary>
        public string AppSecretKey { get; set; }
    }
}
