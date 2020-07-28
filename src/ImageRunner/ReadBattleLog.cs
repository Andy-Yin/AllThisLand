using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BattleEngine;
using Core.BaiDuAI;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Interface;
using Newtonsoft.Json;

namespace ImageRunner
{
    public class ReadBattleLog
    {
        private static Baidu.Aip.Ocr.Ocr client = new Baidu.Aip.Ocr.Ocr(BaiduKey.API_KEY, BaiduKey.SECRET_KEY);
        private readonly IHeroRepository _heroRepository;

        public ReadBattleLog(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task GeneralBattleLog()
        {
            var hero1 = new Hero();
            var hero2 = new Hero();
            var hero3 = new Hero();
            var hero4 = new Hero();
            var hero5 = new Hero();
            var hero6 = new Hero();
            var battleHeroList = new List<Hero>();
            battleHeroList.Add(hero1);
            battleHeroList.Add(hero2);
            battleHeroList.Add(hero3);
            battleHeroList.Add(hero4);
            battleHeroList.Add(hero5);
            battleHeroList.Add(hero6);

            var heroListInDb = await _heroRepository.AllAsync();
            // 读取所有文件名
            var imgList = GetFilesIncludingSubfolders(Common.BattleLogPath);

            // 读取所有图片
            foreach (var imgPath in imgList)
            {
                BaiduAIResult model = BaiDuHelper.ParseImage(client, imgPath);

                foreach (var wordsArray in model.words_result)
                {
                    
                }
                // 识别出武将的数据
                var hero = new T_Hero();


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
}
