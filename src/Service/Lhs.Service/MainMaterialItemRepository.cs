using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

namespace Lhs.Service
{
    /// <summary>
    /// 主材基础数据
    /// </summary>
    public class MainMaterialItemRepository : PlatformBaseService<T_MainMaterialItem>, IMainMaterialItemRepository
    {
        public MainMaterialItemRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        public async Task<List<T_MainMaterialItem>> GetList()
        {
            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_MainMaterialItem WHERE IsDel=0";
                return (await coon.QueryAsync<T_MainMaterialItem>(sql)).ToList();
            }
        }

        public async Task<List<MainMaterialItemWithCategory>> GetMaterialItemWithCategoryList()
        {
            using (var coon = Connection)
            {
                var sql = @"select mmi.*, mmc.Name CategoryName from T_MainMaterialItem mmi
                left join T_MainMaterialCategory mmc on mmi.CategoryId = mmc.Id
                where mmi.IsDel = 0 and mmc.IsDel = 0                ";
                var list = (await coon.QueryAsync<MainMaterialItemWithCategory>(sql)).ToList();

                // 因为多个质检用竖线隔开了，所以需要再拆开：淋浴房	D04|D05|D08,要非诚三条
                var resultList = new List<MainMaterialItemWithCategory>();
                foreach (var item in list)
                {
                    if (item.Code.Contains("|"))
                    {
                        var sub = item.Code.Split("|").ToList();
                        foreach (string s in sub)
                        {
                            var material = new MainMaterialItemWithCategory();
                            material.Code = s;
                            material.Name = item.Name;
                            material.CategoryId = item.CategoryId;
                            material.CategoryName = item.CategoryName;
                            resultList.Add(material);
                        }
                    }
                    else
                    {
                        resultList.Add(item);
                    }
                }

                return resultList;
            }
        }
    }
}
