using Core.Sms.TencentSMSSDK;
using Core.Util;
using System;

namespace Lhs.Common
{
    public class SmsHelper
    {
        /// <summary>
        /// 发送短信调用
        /// </summary>
        public static bool SendMessage(string phone, string content)
        {
            var resultModel = SelfTencentSmsHelper.SendMsgWithTemplate(phone, content);
            var result = resultModel.result;
            if (result == 0) return true;
            Log4NetHelper.Error(resultModel);
            return false;
        }
    }

}
