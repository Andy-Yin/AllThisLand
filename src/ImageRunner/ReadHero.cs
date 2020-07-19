using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core.BaiDuAI;
using Lhs.Interface;

namespace ImageRunner
{
    public class ReadHero
    {
        private static Baidu.Aip.Ocr.Ocr client = new Baidu.Aip.Ocr.Ocr(BaiduKey.API_KEY, BaiduKey.SECRET_KEY);
        private readonly IHeroRepository _heroRepository;

        public ReadHero(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public static void GeneralHeroInfo()
        {
            var image = File.ReadAllBytes("C:\\Users\\v-cunyin\\Desktop\\率土之滨\\1.jpg");

            // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
            var result = client.GeneralBasic(image);
            Console.WriteLine(result);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"language_type", "CHN_ENG"},
                {"detect_direction", "true"},
                {"detect_language", "true"},
                {"probability", "true"}
            };
            // 带参数调用通用文字识别, 图片参数为本地图片
            result = client.GeneralBasic(image, options);
            Console.WriteLine(result);
        }
    }
}
