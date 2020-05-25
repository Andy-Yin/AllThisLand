using System.Configuration;

namespace Lhs.Common
{
    /// <summary>
    /// 先临时放到这个位置，后续再调整
    /// </summary>
    public static class CommonMessage
    {
        public static string GetFailure = "获取失败";
        public static string LoginFailed = "登录失败";
        public static string GetSuccess = "获取成功";
        public static string UserNotExistOrPwdError = "用户不存在或者密码错误";
        public static string UserNotExist = "用户不存在";
        public static string OperateSuccess = "操作成功";
        public static string OperateFailed = "操作失败";
        public static string OldPasswordError = "原密码不正确";
        public static string NoPermissionForApp = "用户身份不是工长或者监理";
        public static string PackageExist = "报价单已存在！";
        public static string FlowPositionNotExist = "对应的用户不存在！";
        public static string ApproveAuthFail = "没有审核权限。";
        public static string CannotStartDueToMoney = "收款金额未达到要求，不可开工。";
        public static string SubmitFailure = "提交失败";
        public static string SubmitSuccess = "提交成功";
        public static string MaterialCodeNotEmpty = "名称或者编码不能为空。";
        public static string MaterialCodeExisted = "编码已存在。";


        /// <summary>
        /// 年-月-日
        /// </summary>
        public static string DateFormatYMD = "yyyy-MM-dd";

        /// <summary>
        /// 年-月-日 时：分：秒
        /// </summary>
        public static string DateFormatYMDHM = "yyyy-MM-dd HH:mm";

        /// <summary>
        /// 年-月-日 时：分：秒
        /// </summary>
        public static string DateFormatYMDHMS = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 详细格式
        /// </summary>
        public static string DateFormatYYYYMMDDHHSSFFFF = "yyyyMMddHHmmssffff";
    }

}
