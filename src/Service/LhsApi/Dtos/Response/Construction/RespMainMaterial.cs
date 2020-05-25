using System.Collections.Generic;
using System.Linq;
using Lhs.Common.Const;
using Lhs.Entity.DbEntity.DbModel;
using Lhs.Entity.ForeignDtos.Response.Construction;
using Lhs.Entity.ForeignDtos.Response.Disclosure;

namespace LhsAPI.Dtos.Response.Disclosure
{
    /// <summary>
    /// 主材基础数据列表
    /// </summary>
    public class MainMaterialBasicCategory
    {
        public MainMaterialBasicCategory()
        {
        }

        public MainMaterialBasicCategory(T_MainMaterialCategory category)
        {
            CategoryId = category.Id;
            CategoryName = category.Name;
        }

        /// <summary>
        /// id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public List<MainMaterialBasic> MaterialList { get; set; }
    }

    public class MainMaterialBasic
    {
        public MainMaterialBasic()
        {
        }

        public MainMaterialBasic(T_MainMaterialItem item)
        {
            MaterialId = item.Id;
            MaterialName = item.Name;
            Code = item.Code.Split(CommonConst.Separator).ToList();
        }

        /// <summary>
        /// id
        /// </summary>
        public int MaterialId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public List<string> Code { get; set; }
    }
}
