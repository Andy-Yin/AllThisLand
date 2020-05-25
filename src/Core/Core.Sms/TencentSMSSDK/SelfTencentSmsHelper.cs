using System.Collections.Generic;
using Core.Sms.TencentSMSSDK.Sms;

namespace Core.Sms.TencentSMSSDK
{
    public class SelfTencentSmsHelper
    {
        private static readonly SmsSingleSender _smsSingleSender = new SmsSingleSender();
        private static readonly SmsMultiSender _smsMultileSender = new SmsMultiSender();

        /// <summary>
        /// 指定模板单发
        /// </summary>
        public static SmsSingleSenderResult SendMsgWithTemplate(string phone, string msg)
        {
            var result = _smsSingleSender.Send(0, "86", phone, msg, "", "");

            return result;
        }

        /// <summary>
        /// 带参数指定模板单发
        /// </summary>
        /// <param name="phone">接收短信的手机号</param>
        /// <param name="paramList">模板对应的参数</param>
        /// <param name="templateId">模板id</param>
        /// <returns>0表示成功(计费依据)，非0表示失败</returns>
        public static SmsSingleSenderResult SendMsgWithTemplateParam(string phone, List<string> paramList, int templateId = 0)
        {
            if (templateId <= 0)
                templateId = 54875;
            var result = _smsSingleSender.SendWithParam("86", phone, templateId, paramList, "", "", "");

            return result;
        }

        /// <summary>
        /// 群发短信
        /// </summary>
        public static object SendMultMsgWithTemplate(List<string> phoneNumbers, string msg)
        {
           
            var result = _smsMultileSender.Send(0, "86", phoneNumbers, msg, "", "");

            return result;
        }

        /// <summary>
        /// 带参数指定模板群发
        /// </summary>
        public static object SendMulMsgWithTemplateParam(List<string> phoneNumbers, List<string> paramList, int templateId = 0)
        {
            if (templateId <= 0)
                templateId = 54875;
            var result = _smsMultileSender.SendWithParam("86", phoneNumbers, templateId, paramList, "", "", "");
            return result;
        }

    }
}