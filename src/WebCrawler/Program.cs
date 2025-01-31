using Core.Util;
using HtmlAgilityPack;
using Lhs.Entity.DbEntity.DbModel;
using System;
using System.Net;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;

class Program
{
    static async Task Main()
    {
        string baseUrl = "https://stzb.163.com/herolist/";
        var heroList = new List<T_Hero>();

        // 将herolist的信息爬取下来
        // 将herolist获取的信息存储到数据库中，这里使用的是sqlite，可以使用dapper，service在Lhs.servece这个项目，实体在Lhs.Entity项目，repository模式
        // 并添加单元测试，
        // a. 测试是否能够成功获取到herolist的信息，
        // b. 测试是否能够成功存储到数据库中，存储一个或者批量存储
        // c. 测试是否能够成功从数据库中获取到herolist的信息，获取一个或者批量获取
        // d. 测试是否能够成功更新herolist的信息，更新一个或者批量更新
        // e. 测试是否能够成功删除herolist的信息，
        // f. 测试是否能够成功获取herolist的信息的总数

        // 初始化数据库连接和仓储类
        string connectionString = "Data Source=heroes.db";
        var heroRepository = new HeroRepository(connectionString);

        // 确保数据库和表存在
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS T_Hero (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Desc TEXT,
                    Cost REAL,
                    Star INTEGER,
                    Gender BOOLEAN,
                    Country INTEGER,
                    Corps INTEGER,
                    AttackRange INTEGER,
                    InitialAttack REAL,
                    InitialDefense REAL,
                    InitialSiege REAL,
                    InitialSpeed REAL,
                    InitialStrategy REAL,
                    DefaultMagic TEXT
                )");
        }

        using HttpClient client = new HttpClient();

        for (int i = 100001; i <= 100358; i++)
        {
            try
            {
                string url = baseUrl + i + ".html";
                string html = await client.GetStringAsync(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                string name = doc.DocumentNode.SelectSingleNode("//div[@class='role-content']/h1").InnerText;
                string desc = doc.DocumentNode.SelectSingleNode("//p[@class='desc']").InnerText.Trim();

                // 获取星级
                string starClass = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'star')]/i").GetAttributeValue("class", "");
                int starLevel = 5; // 默认为5星
                if (starClass.Contains("star-3"))
                {
                    starLevel = 3;
                }
                else if (starClass.Contains("star-4"))
                {
                    starLevel = 4;
                }

                var tmpCost = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), 'cost')]").InnerText.Split('：')[1];
                double cost = Convert.ToDouble(tmpCost);
                string tmpTroopType = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '兵种')]").InnerText.Trim().Split('：')[1];
                EnumCorps troopType;
                if (tmpTroopType == "弓")
                {
                    troopType = EnumCorps.CorpsArcher;
                }
                else if (tmpTroopType == "步")
                {
                    troopType = EnumCorps.CorpsInfantry;
                }
                else
                {
                    troopType = EnumCorps.CorpsCavalry;
                }

                var tmpRange = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '攻击距离')]").InnerText.Trim().Split('：')[1];
                int attackRange = Convert.ToInt32(tmpRange);

                var tmpStrategy = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), '初始谋略')]").InnerText.Split('：')[1];
                double initialStrategy = Convert.ToDouble(tmpStrategy);

                var tmpAttack = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), '初始攻击')]").InnerText.Split('：')[1];
                double initialAttack = Convert.ToDouble(tmpAttack);
                var tmpSiege = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始攻城')]").InnerText.Trim().Split('：')[1];
                double initialSiege = Convert.ToDouble(tmpSiege);

                var tmpDefense = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始防御')]").InnerText.Trim().Split('：')[1];
                double initialDefense = Convert.ToDouble(tmpDefense);
                var tmpSpeed = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始速度')]").InnerText.Trim().Split('：')[1];
                double initialSpeed = Convert.ToDouble(tmpSpeed);

                string initialMagic = doc.DocumentNode.SelectSingleNode("//dt[contains(text(), '基础战法')]").SelectSingleNode("following-sibling::dd[1]").InnerText.Trim();
                string decomposeMagic = doc.DocumentNode.SelectSingleNode("//dt[contains(text(), '可拆战法')]").SelectSingleNode("following-sibling::dd[1]").InnerText.Trim();

                var hero = new T_Hero
                {
                    Name = name,
                    Desc = desc,
                    Star = starLevel,
                    Cost = cost,
                    AttackRange = attackRange,
                    Corps = troopType,
                    InitialStrategy = initialStrategy,
                    InitialAttack = initialAttack,
                    InitialSiege = initialSiege,
                    InitialDefense = initialDefense,
                    InitialSpeed = initialSpeed,
                    DefaultMagic = initialMagic
                };

                heroList.Add(hero);

                Console.WriteLine("------------" + i + "--------------");
            }
            catch (Exception e)
            {
                Console.WriteLine("-=====================----------" + e.Message + i + "--===========================------------");
            }
        }

        // 将爬取到的英雄信息存储到数据库中
        foreach (var hero in heroList)
        {
            await heroRepository.InsertHeroAsync(hero);
        }

        // 打印所有的herolist的信息 
        foreach (var hero in heroList)
        {
            Console.WriteLine(hero.ToJson().ToString());
        }

        // 示例：从数据库中获取所有英雄信息
        var allHeroes = await heroRepository.GetAllHeroesAsync();
        Console.WriteLine("从数据库中获取的所有英雄信息：");
        foreach (var hero in allHeroes)
        {
            Console.WriteLine(hero.ToJson().ToString());
        }
    }
}
