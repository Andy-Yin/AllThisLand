namespace Core.Util.SettingModel
{
    /// <summary>
    /// AppSetting
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// GrandParent_Key
        /// </summary>
        public GrandParent GrandParent_Key { get; set; }

        /// <summary>
        /// Parent_Key
        /// </summary>
        public ParentKeySetting Parent_Key { get; set; }

        /// <summary>
        /// Child_Key
        /// </summary>
        public string Child_Key { get; set; }
    }

    /// <summary>
    /// GrandParent
    /// </summary>
    public class GrandParent
    {
        /// <summary>
        /// ParentKeySetting
        /// </summary>
        public ParentKeySetting Parent_Key { get; set; }
    }

    /// <summary>
    /// ParentKeySetting
    /// </summary>
    public class ParentKeySetting
    {
        /// <summary>
        /// Child_Key
        /// </summary>
        public string Child_Key { get; set; }
    }
}
