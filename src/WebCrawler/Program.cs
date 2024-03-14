using HtmlAgilityPack;
using System;
using System.Net;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        string baseUrl = "https://stzb.163.com/herolist/";

        for (int i = 100001; i <= 100358; i++)
        {
            try
            {
                string url = baseUrl + i + ".html";
                WebClient client = new WebClient();
                string html = client.DownloadString(url);

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

                string cost = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), 'cost')]").InnerText.Split('：')[1];
                string troopType = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '兵种')]").InnerText.Trim().Split('：')[1];
                string attackRange = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '攻击距离')]").InnerText.Trim().Split('：')[1];

                string initialStrategy = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), '初始谋略')]").InnerText.Split('：')[1];
                string initialAttack = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), '初始攻击')]").InnerText.Split('：')[1];
                string initialSiege = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始攻城')]").InnerText.Trim().Split('：')[1];

                string initialDefense = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始防御')]").InnerText.Trim().Split('：')[1];
                string initialSpeed = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始速度')]").InnerText.Trim().Split('：')[1];

                string initialMagic = doc.DocumentNode.SelectSingleNode("//dt[contains(text(), '基础战法')]").SelectSingleNode("following-sibling::dd[1]").InnerText.Trim();
                string decomposeMagic = doc.DocumentNode.SelectSingleNode("//dt[contains(text(), '可拆战法')]").SelectSingleNode("following-sibling::dd[1]").InnerText.Trim();

                Console.WriteLine("Name: " + name);
                Console.WriteLine("desc: " + desc);
                Console.WriteLine("星级:" + starLevel);

                Console.WriteLine("troopType兵种: " + troopType);
                Console.WriteLine("Cost: " + cost);
                Console.WriteLine("Attack Range攻击距离: " + attackRange);
                Console.WriteLine("Initial Strategy初始谋略: " + initialStrategy);
                Console.WriteLine("Initial Attack初始攻击: " + initialAttack);
                Console.WriteLine("Initial Siege初始攻城: " + initialSiege);
                Console.WriteLine("Initial Defense初始防御: " + initialDefense);
                Console.WriteLine("Initial Speed初始速度: " + initialSpeed);
                Console.WriteLine("基础战法: " + initialMagic);
                Console.WriteLine("可拆战法: " + decomposeMagic);

                //Console.WriteLine("Name: " + name + " desc: " + desc + " Cost: " + cost + " Attack Range: " + attackRange + " Initial Strategy: " + initialStrategy + " Initial Attack: " + initialAttack + " Initial Siege: " + initialSiege + " Initial Defense: " + initialDefense + " Initial Speed: " + initialSpeed);


                Console.WriteLine("------------" + i + "--------------");
            }
            catch (Exception e)
            {

                Console.WriteLine("------------" + e.Message + "--------------");
            }
            
        }
    }
}
