using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
           
            // var heroImgFolder = Path.Combine(basePath, baseHeroImgFolder);
            // 读取所有文件名
            var imgList = GetFilesIncludingSubfolders();

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

                var logList = CleanUpLog(model);

                StringBuilder sb = new StringBuilder();
                foreach (var log in logList)
                {
                    sb.AppendLine(log.ToString());
                }


                //ParseLogList(logList);

                // // 识别出武将的数据

                //foreach (var item in logList)
                // {
                //     Console.WriteLine(item.ToString());
                // }

                var resultStr = sb.ToString();
                Thread.Sleep(500);
            }

        }

        private  List<string> CleanUpLog(BaiduAIResult model)
        {
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
                if (temp == "X" || temp == "战报详情")
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
            return logList;
        }

        private static void ParseLogList(List<string> logList)
        {
            foreach (var item in logList)
            {
                // 使用正则表达式匹配【】中的内容:string input = "【王异】【众谋不懈】的效果使八【呈甫嵩】损失了891兵力(9099)";
                // 按照"【"、"】"和"损失了"来切分字符串
                string[] parts = item.Split(new string[] { "【", "】", "损失了" }, StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine(item.ToString());
                // 输出切分出的内容
                foreach (string part in parts)
                {
                    Console.WriteLine(part);
                }
                //Regex regex = new Regex(@"【(.*?)】");
                //MatchCollection matches = regex.Matches(item);

                //// 输出匹配到的内容
                //foreach (Match match in matches)
                //{
                //    Console.WriteLine(match.Groups[1].Value);
                //}
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
        /// 文件夹目录为程序运行目录的"BattleImagePath"，
        /// 如果没有该文件夹则创建该文件夹，如果内部没有文件，则返回空List。
        /// </summary>
        private static List<string> GetFilesIncludingSubfolders(string searchPattern = "*.jpg")
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BattleImagePath");
            List<string> paths = new List<string>();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            paths.AddRange(Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories).ToList());
            return paths;
        }

    }

    public class FightingHero {
            
    }
}
