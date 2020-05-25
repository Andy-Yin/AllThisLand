using Dapper;
using Microsoft.Extensions.Configuration;
using Lhs.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;
using OfficeOpenXml.Drawing.Chart;

namespace Lhs.Service
{
    /// <summary>
    /// 地采基础数据
    /// </summary>
    public class LocalMaterialItemRepository : PlatformBaseService<T_LocalMaterialItem>, ILocalMaterialItemRepository
    {
        public LocalMaterialItemRepository(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        public async Task<bool> DeleteBasicItem(List<int> ids)
        {
            using (var coon = Connection)
            {
                var sql = $"UPDATE dbo.T_LocalMaterialItem SET IsDel=1,EditTime=GETDATE() WHERE Id IN ({string.Join(",", ids)})";
                var result = await coon.ExecuteAsync(sql);
                return result > 0;
            }
        }

        public async Task<List<T_LocalMaterialItem>> GetList()
        {

            using (var coon = Connection)
            {
                var sql = "SELECT * FROM dbo.T_LocalMaterialItem WHERE IsDel=0";
                var list = (await coon.QueryAsync<T_LocalMaterialItem>(sql)).ToList();

                // 因为多个质检用竖线隔开了，所以需要再拆开：淋浴房	D04|D05|D08,要非诚三条
                var resultList = new List<T_LocalMaterialItem>();
                foreach (var item in list)
                {
                    if (item.Code.Contains("|"))
                    {
                        var sub = item.Code.Split("|").ToList();
                        foreach (string s in sub)
                        {
                            var material = new T_LocalMaterialItem();
                            material.Code = s;
                            material.Name = item.Name;
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
