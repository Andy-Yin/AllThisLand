//using OfficeOpenXml;
//using System;
//using System.IO;
//using System.Net;
//using HtmlAgilityPack;

//class Program
//{
//    static void Main()
//    {
//        string baseUrl = "https://www.example.com/heroes/";
//        string filePath = @"D:\heroes.xlsx";


//        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//        // 创建一个新的 Excel 包
//        var excelPackage = new ExcelPackage();
//        var ws = excelPackage.Workbook.Worksheets.Add("hero"); // 添加名为 hero 的工作表

//        // 写入表头
//        WriteHeader(ws);

//        // 爬取数据并写入 Excel
//        CrawlAndWriteData(baseUrl, ws);

//        // 保存 Excel 文件到指定位置
//        SaveExcelFile(excelPackage, filePath);

//        Console.WriteLine("数据已写入 Excel 文件：" + filePath);
//    }

//    static void WriteHeader(ExcelWorksheet ws)
//    {
//        ws.Cells["A1"].Value = "Name";
//        ws.Cells["B1"].Value = "Description";
//        ws.Cells["C1"].Value = "Star Level";
//        ws.Cells["D1"].Value = "Troop Type";
//        ws.Cells["E1"].Value = "Cost";
//        ws.Cells["F1"].Value = "Attack Range";
//        ws.Cells["G1"].Value = "Initial Strategy";
//        ws.Cells["H1"].Value = "Initial Attack";
//        ws.Cells["I1"].Value = "Initial Siege";
//        ws.Cells["J1"].Value = "Initial Defense";
//        ws.Cells["K1"].Value = "Initial Speed";
//        ws.Cells["L1"].Value = "Initial Magic";
//        ws.Cells["M1"].Value = "Decompose Magic";
//    }

//    static void CrawlAndWriteData(string baseUrl, ExcelWorksheet ws)
//    {
//        int row = 2; // 从第二行开始写入数据

//        for (int i = 100001; i <= 100358; i++)
//        {
//            try
//            {
//                string url = baseUrl + i + ".html";
//                WebClient client = new WebClient();
//                string html = client.DownloadString(url);

//                HtmlDocument doc = new HtmlDocument();
//                doc.LoadHtml(html);

//                string name = doc.DocumentNode.SelectSingleNode("//div[@class='role-content']/h1").InnerText;
//                string desc = doc.DocumentNode.SelectSingleNode("//p[@class='desc']").InnerText.Trim();

//                // 获取星级
//                string starClass = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'star')]/i").GetAttributeValue("class", "");
//                int starLevel = 5; // 默认为5星
//                if (starClass.Contains("star-3"))
//                {
//                    starLevel = 3;
//                }
//                else if (starClass.Contains("star-4"))
//                {
//                    starLevel = 4;
//                }

//                string cost = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), 'cost')]").InnerText.Split('：')[1];
//                string troopType = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '兵种')]").InnerText.Trim().Split('：')[1];
//                string attackRange = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '攻击距离')]").InnerText.Trim().Split('：')[1];

//                string initialStrategy = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), '初始谋略')]").InnerText.Split('：')[1];
//                string initialAttack = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]//span[contains(text(), '初始攻击')]").InnerText.Split('：')[1];
//                string initialSiege = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始攻城')]").InnerText.Trim().Split('：')[1];

//                string initialDefense = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始防御')]").InnerText.Trim().Split('：')[1];
//                string initialSpeed = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'attr-list')]/span[contains(text(), '初始速度')]").InnerText.Trim().Split('：')[1];

//                string initialMagic = doc.DocumentNode.SelectSingleNode("//dt[contains(text(), '基础战法')]").SelectSingleNode("following-sibling::dd[1]").InnerText.Trim();
//                string decomposeMagic = doc.DocumentNode.SelectSingleNode("//dt[contains(text(), '可拆战法')]").SelectSingleNode("following-sibling::dd[1]").InnerText.Trim();

//                // 写入到 Excel
//                WriteDataToExcel(ws, row, name, desc, starLevel, troopType, cost, attackRange, initialStrategy, initialAttack, initialSiege, initialDefense, initialSpeed, initialMagic, decomposeMagic);

//                row++; // 移动到下一行

//                Console.WriteLine("------------" + i + "--------------");
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("-=====================----------" + e.Message + i + "--===========================------------");
//            }
//        }
//    }

//    static void WriteDataToExcel(ExcelWorksheet ws, int row, string name, string desc, int starLevel, string troopType, string cost, string attackRange, string initialStrategy, string initialAttack, string initialSiege, string initialDefense, string initialSpeed, string initialMagic, string decomposeMagic)
//    {
//        ws.Cells["A" + row].Value = name;
//        ws.Cells["B" + row].Value = desc;
//        ws.Cells["C" + row].Value = starLevel;
//        ws.Cells["D" + row].Value = troopType;
//        ws.Cells["E" + row].Value = cost;
//        ws.Cells["F" + row].Value = attackRange;
//        ws.Cells["G" + row].Value = initialStrategy;
//        ws.Cells["H" + row].Value = initialAttack;
//        ws.Cells["I" + row].Value = initialSiege;
//        ws.Cells["J" + row].Value = initialDefense;
//        ws.Cells["K" + row].Value = initialSpeed;
//        ws.Cells["L" + row].Value = initialMagic;
//        ws.Cells["M" + row].Value = decomposeMagic;
//    }

//    static void SaveExcelFile(ExcelPackage excelPackage, string filePath)
//    {
//        FileInfo excelFile = new FileInfo(filePath);
//        excelPackage.SaveAs(excelFile);
//    }
//}
