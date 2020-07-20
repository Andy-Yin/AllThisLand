using System;
using System.Collections.Generic;
using System.IO;
using Core.BaiDuAI;
using Lhs.Interface;
using Lhs.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ImageRunner
{
    class Program
    {
        private readonly IHeroRepository _heroRepository;
        //client.Timeout = 60000;  // 修改超时时间
        static void Main(string[] args)
        {
            //GeneralBasicDemo();

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IHeroRepository, HeroRepository>()
                .BuildServiceProvider();

            ReadHero readHero = new ReadHero(serviceProvider.GetService<HeroRepository>());
            readHero.GeneralHeroInfo();
        }


     
        //public void GeneralBasicUrlDemo()
        //{
        //    var url = "https//www.x.com/sample.jpg";

        //    // 调用通用文字识别, 图片参数为远程url图片，可能会抛出网络等异常，请使用try/catch捕获
        //    var result = client.GeneralBasicUrl(url);
        //    //Console.WriteLine(result);
        //    // 如果有可选参数
        //    var options = new Dictionary<string, object>{
        //        {"language_type", "CHN_ENG"},
        //        {"detect_direction", "true"},
        //        {"detect_language", "true"},
        //        {"probability", "true"}
        //    };
        //    // 带参数调用通用文字识别, 图片参数为远程url图片
        //    result = client.GeneralBasicUrl(url, options);
        //    //Console.WriteLine(result);
        //}
    }
}
