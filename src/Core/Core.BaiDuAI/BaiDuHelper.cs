using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.BaiDuAI
{
    public class BaiDuHelper
    {
        /// <summary>
        /// 用百度接口读取图片内容
        /// </summary>
        /// <param name="client"></param>
        /// <param name="imgPath">图片地址</param>
        /// <returns></returns>
        public static BaiduAIResult ParseImage(Baidu.Aip.Ocr.Ocr client, string imgPath)
        {
            var image = File.ReadAllBytes(imgPath);

            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasic(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>
            {
                {"language_type", "CHN_ENG"},
                {"detect_direction", "true"},
                {"detect_language", "true"},
                {"probability", "false"}
            };
            // 带参数调用通用文字识别, 图片参数为本地图片
            result = client.GeneralBasic(image, options);
            BaiduAIResult model = result.ToObject<BaiduAIResult>();
            return model;
        }
    }
}
