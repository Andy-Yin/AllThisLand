using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LhsApi.Dtos.Request.Quality
{
    public class ReqAddQualityRecord : ReqAuth
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 提交的监理的Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 具体条目的Id（三级分类Id）
        /// </summary>
        public List<int> ItemIds { get; set; }

        /// <summary>
        /// 要求整改日期
        /// </summary>
        public DateTime RectDate { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [Required]
        public string Note { get; set; }

        /// <summary>
        /// 附件图片,多个用|隔开
        /// </summary>
        [Required]
        public string Images { get; set; }
    }
}
