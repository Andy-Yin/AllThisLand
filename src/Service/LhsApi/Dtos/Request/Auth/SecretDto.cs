namespace LhsAPI.Authorization.Secret.Dto.Auth
{
    public class SecretDto
    {
        /// <summary>
        /// 账号名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 登录后授权的 Token
        /// </summary>
        public string Token { get; set; }

    }
}
