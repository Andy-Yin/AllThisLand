namespace Core.Util.SettingModel
{
    /// <summary>
    /// appsetting.json对应的设置类
    /// </summary>
    public class AllSetting
    {
        ///// <summary>
        ///// 数据库配置
        ///// </summary>
        public ConnectionSetting ConnectionStrings { get; set; }

        ///// <summary>
        ///// 日志模块
        ///// </summary>
        public LoggingSetting Logging { get; set; }

        ///// <summary>
        ///// 跨域相关
        ///// </summary>
        public string AllowedHosts { get; set; }

        ///// <summary>
        ///// 网关配置
        ///// </summary>
        public GetWaySetting Gateway { get; set; }

        ///// <summary>
        ///// 缓存配置
        ///// </summary>
        public EasyCachingSetting EasyCaching { get; set; }

        ///// <summary>
        ///// app 设置
        ///// </summary>
        public AppSetting AppSetting { get; set; }

        ///// <summary>
        ///// jwt认证相关
        ///// </summary>
        public JwtSetting Jwt { get; set; }

        ///// <summary>
        ///// 文件服务器
        ///// </summary>
        public string FileServer { get; set; }

        ///// <summary>
        ///// 图片的上传路径
        ///// </summary>
        public string ImgUploadPath { get; set; }

        ///// <summary>
        ///// 数据库中存储的图片路径
        ///// </summary>
        public string ImgSavePath { get; set; }

        ///// <summary>
        ///// 腾讯云配置
        ///// </summary>
        public TencentCloudSeting TencentCloud { get; set; }
    }
}
