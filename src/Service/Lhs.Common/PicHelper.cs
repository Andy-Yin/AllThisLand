using System;

namespace Lhs.Common
{
    public class PicHelper
    {
        private static readonly string FileServer = ConfigurationHelper.AppSetting["FileServer"];

        /// <summary>
        /// 拼接图片地址的方法
        /// </summary>
        public static string ConcatPicUrl(string imgUrl)
        {
            if (!string.IsNullOrEmpty(imgUrl))
                imgUrl = $@"{FileServer}{imgUrl}";
            else
                imgUrl = string.Empty;
            return imgUrl;
        }

        /// <summary>
        /// 根据全路径获取相对地址（不带域名）
        /// </summary>
        public static string GetLocalPath(string imgUrl)
        {
            try
            {
                var url = new Uri(imgUrl);
                return url.LocalPath;
            }
            catch
            {
                return imgUrl;
            }
        }
    }

}
