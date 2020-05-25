using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lhs.Entity.ForeignDtos.Response
{
    /// <summary>
    /// 获取试卷列表
    /// </summary>
    public class GetPaperList
    {
        /// <summary>
        /// 试卷列表
        /// </summary>
        public List<GetPaperListModelForH5> PaperList = new List<GetPaperListModelForH5>();

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { set; get; }

        /// <summary>
        /// 用户状态 -1：游客 0：注册会员 1：报名学员
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 试卷列表的实体
    /// </summary>
    public class GetPaperListModelForH5
    {
        /// <summary>
        /// 试卷ID
        /// </summary>
        public int PaperId { set; get; }

        /// <summary>
        /// 试卷名称
        /// </summary>
        public string PaperName { set; get; }

        /// <summary>
        /// 班级名称
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { set; get; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; } = string.Empty;

        /// <summary>
        /// 用户做该试卷的状态：1：未开始 2：继续做题 3：已做过 
        /// </summary>
        public int Status { set; get; }

        /// <summary>
        /// 做完的次数:已做多少次”&&”未完成 不定次
        /// </summary>
        public string FinishedCount { set; get; }

        /// <summary>
        /// 是否公开试卷
        /// </summary>
        public bool IsPublished { get; set; } = false;
    }
}
