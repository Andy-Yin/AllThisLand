using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Core.BaiDuAI;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Interface;
using Newtonsoft.Json;

namespace ImageRunner
{
    public class ReadHero
    {
        private static Baidu.Aip.Ocr.Ocr client = new Baidu.Aip.Ocr.Ocr(BaiduKey.API_KEY, BaiduKey.SECRET_KEY);

        public ReadHero()
        {
        }

        public void GeneralHeroInfo()
        {
            //var baseHeroImgFolder = "\\Img\\Hero\\";
            var basePath = "D:\\test\\"; // Directory.GetCurrentDirectory();
            // var heroImgFolder = Path.Combine(basePath, baseHeroImgFolder);
            // 读取所有文件名
            var imgList = GetFilesIncludingSubfolders(basePath);

            // 读取所有图片
            foreach (var imgPath in imgList)
            {
                var image = File.ReadAllBytes(imgPath);

                // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获
                var result = client.GeneralBasic(image);
                //Console.WriteLine(result);
                // 如果有可选参数
                var options = new Dictionary<string, object>{
                    {"language_type", "CHN_ENG"},
                    {"detect_direction", "true"},
                    {"detect_language", "true"},
                    {"probability", "false"}
                };
                // 带参数调用通用文字识别, 图片参数为本地图片
                result = client.GeneralBasic(image, options);

                //Console.WriteLine(result);
                BaiduAIResult model = result.ToObject<BaiduAIResult>();

                List<string> logList = new List<string>();
                foreach (var item in model.words_result)
                {
                    // 去掉空格
                    string temp = item.Words.Trim();
                    temp = temp.Replace("、", "");
                    temp = temp.Replace("●", "");
                    temp = temp.Replace("飞【", "【");
                    temp = temp.Replace("乍【", "【");
                    temp = temp.Replace("長【", "【");
                    temp = temp.Replace("！详", ""); 
                    if (temp =="X"|| temp == "战报详情")
                    {
                        continue;
                    }
                    else
                    {
                        if (temp.Contains("【"))
                        {
                            logList.Add(temp);  
                        }
                        else
                        {
                            if (logList.Any())
                            {
                                string continueStr = temp;

                                //如果不是最开始，则拼到上一个里
                                logList[logList.Count - 1] = logList[logList.Count - 1] + continueStr;
                            }
                         
                        }
                       
                    }
                }

                // 识别出武将的数据

               foreach (var item in logList)
                {
                    Console.WriteLine(item.ToString());
                }

                Thread.Sleep(500);
            }

        }

        /// <summary>
        /// 处理成实体
        /// </summary>
        /// <param name="baiduAiResult"></param>
        private void PartToModel(BaiduAIResult baiduAiResult)
        {

        }
        /// <summary>
        /// 获取文件夹下所有图片
        /// </summary>
        private static List<string> GetFilesIncludingSubfolders(string path, string searchPattern= "*.jpg")
        {
            List<string> paths = new List<string>();

            paths.AddRange(Directory.GetFiles(path, searchPattern).ToList());
            return paths;
        }

    }

    public class FightingHero {
            
    }
}
