using System.ComponentModel;
using System.Configuration;
using System.Runtime.Serialization;
using Core.Util;

namespace Lhs.Common.Const
{
    public class CommonConst
    {
        /// <summary>
        /// 上传的图片文件命名格式
        /// </summary>
        public static string ImgDateFormat = "yyyyMMddHHmmssffff";

        /// <summary>
        /// 图片上传路径
        /// </summary>
        public static string ImgUploadPath = ConfigurationHelper.AppSetting["ImgUploadPath"];

        /// <summary>
        /// 图片保存路径
        /// </summary>
        public static string ImgSavePath = ConfigurationHelper.AppSetting["ImgSavePath"];

        /// <summary>
        /// 下载或者其他需要的Response配置
        /// </summary>
        public static class ResponseConfigure
        {
            /// <summary>
            /// 字符集
            /// </summary>
            public static string Charset = "UTF-8";

            /// <summary>
            /// 内容编码
            /// </summary>
            public static string ContentEncoding = "UTF-8";

            /// <summary>
            /// 内容类型为Excel
            /// </summary>
            public static string ContentTypeExcel = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            /// <summary>
            /// 内容配置属性名称
            /// </summary>
            public static string ContentDisposition = "Content-Disposition";

            /// <summary>
            /// 文件类型为附件
            /// </summary>
            public static string Attachment = "attachment";
        }

        #region 发送短信
        public class SendMessage
        {
            /// <summary>
            /// 合伙人加盟短信提醒
            /// </summary>
            public static string MessageRegionTemplateForSubmitCooperationTips = ConfigurationHelper.AppSetting["MessageRegionTemplateForSubmitCooperationTips"];

            /// <summary>
            /// 运营手机号
            /// </summary>
            public static string BusinessPhone = ConfigurationHelper.AppSetting["MessageReceiver"];

            /// <summary>
            /// 新的机构入驻提醒
            /// </summary>
            public static string MessageRegionTemplateForNewTrainTips = ConfigurationHelper.AppSetting["MessageRegionTemplateForNewTrainTips"];

        }
        #endregion

        /// <summary>
        /// 空字符串显示 --
        /// </summary>
        public static string StringNullValue = "--";

        /// <summary>
        /// 是
        /// </summary>
        public static string Yes = "是";

        /// <summary>
        /// 否
        /// </summary>
        public static string No = "否";

        /// <summary>
        /// 分页大小
        /// </summary>
        public static int PageSize = 20;

        /// <summary>
        /// 分割符
        /// </summary>
        public static char Separator = '|';

        /// <summary>
        /// 编号前缀：工人变更
        /// </summary>
        public static string NoForWorkerChange = "CW";

        /// <summary>
        /// 编号前缀：施工管理任务
        /// </summary>
        public static string NoForProjectManageTask = "PT";

        /// <summary>
        /// 编号前缀：派工
        /// </summary>
        public static string AssignWorkerTaskPrefix = "PG";

        /// <summary>
        /// 派工任务名称
        /// </summary>
        public static string AssignWorkerTaskName = "派工任务";

        /// <summary>
        /// 调用U9需要的参数Source值
        /// </summary>
        public static string U9ServiceSource = "JFAPP";

    }
}
