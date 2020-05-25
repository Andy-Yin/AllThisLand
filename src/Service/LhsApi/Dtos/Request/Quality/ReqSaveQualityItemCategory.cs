using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqSaveQualityItemCategory : ReqAuth
    {
        /// <summary>
        /// id：＞0为编辑
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 上级分类的Id
        /// </summary>
        public int ParentId { get; set; }

    }
}
