using System;
using System.Collections.Generic;
using System.Text;

namespace Lhs.Common.Const
{
    /// <summary>
    /// todo 临时先放这里，后续都调整各自的服务里
    /// </summary>
    public class CacheConst
    {
        /// <summary>
        /// 学员录播心跳key前缀
        /// </summary>
        public const string StudentHeartBeatKeyPrefix = "vod:stuHeartBeat:key:";

        /// <summary>
        /// 学员录播心跳data前缀
        /// </summary>
        public const string StudentHeartBeatDataPrefix = "vod:stuHeartBeat:data:";

        /// <summary>
        /// 老师录播心跳key前缀
        /// </summary>
        public const string TeacherHeartBeatKeyPrefix = "vod:teaHeartBeat:key:";

        /// <summary>
        /// 老师录播心跳key前缀
        /// </summary>
        public const string TeacherHeartBeatDataPrefix = "vod:teaHeartBeat:data:";

        /// <summary>
        /// 学员班级录播学习排行前缀
        /// </summary>
        public const string ClassRankPrefix = "vod:classRank:";

        /// <summary>
        /// 学员小节学习记录前缀
        /// </summary>
        public const string StudentSectionRecordPrefix = "vod:stuHis:";

        /// <summary>
        /// 班级基础信息前缀
        /// </summary>
        public const string ClassBasicPrefix = "basic:class:";

        /// <summary>
        /// 课程基础信息前缀
        /// </summary>
        public const string CourseBasicPrefix = "basic:course:";

        /// <summary>
        /// 机构基础信息前缀
        /// </summary>
        public const string SchoolBasicPrefix = "basic:school:";

        /// <summary>
        /// 学员基础信息前缀
        /// </summary>
        public const string StudentBasicPrefix = "basic:stu:";

        /// <summary>
        /// 讲师基础信息前缀（通过讲师id获取）
        /// </summary>
        public const string TeacherBasicByTeacherIdPrefix = "basic:teacher:tid:";

        /// <summary>
        /// 讲师基础信息前缀（通过用户id获取）
        /// </summary>
        public const string TeacherBasicByUserIdPrefix = "basic:teacher:uid:";

        /// <summary>
        /// 一个月对应的分钟数
        /// </summary>
        public const int MinutesOfMonth = 60 * 24 * 30;

        /// <summary>
        /// 心跳的关键词
        /// </summary>
        public const string HeartBeat = "HeartBeat";

        /// <summary>
        /// 录播心跳维护到数据库的消息队列
        /// </summary>
        public const string MessageQueue = "vod:mq";

        /// <summary>
        /// 平台后台token
        /// </summary>
        public const string PlatformToken = "token:platform:uid:";
    }
}
