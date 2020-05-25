namespace Core.Util.SettingModel
{
    /// <summary>
    /// 日志模块
    /// </summary>
    public class LoggingSetting
    {
        public LogLevelSetting LogLevel { get; set; } = new LogLevelSetting();
    }

    /// <summary>
    /// 日志水平
    /// </summary>
    public class LogLevelSetting
    {
        public string Default { get; set; }
    }
}
