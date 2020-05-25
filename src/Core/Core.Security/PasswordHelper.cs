using Core.Security;
using Core.Util;

namespace FrameWork.Common.Security
{
    public static class PasswordHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password">明文密码</param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string Encrypt(string password, string secretKey)
        {
            string ret = EncryptMD5Password(password.ToMD5(), secretKey);
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="md5Password">经过md5加密的密码</param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static string EncryptMD5Password(string md5Password, string secretKey)
        {
            secretKey = secretKey.ToMD5().Substring(0, 16);
            string encryptedPassword = EncryptHelper.AESEncrypt(md5Password.ToLower(), secretKey).ToLower();
            string ret = encryptedPassword.ToMD5().ToLower();
            return ret;
        }
    }
}
