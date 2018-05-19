using JuCheap.Core.Infrastructure.Enums;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 登陆返回数据
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 是否登陆成功
        /// </summary>
        public bool LoginSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 登陆结果
        /// </summary>
        public LoginResult Result { get; set; }

        /// <summary>
        /// 返回的登陆用户数据
        /// </summary>
        public UserDto User { get; set; }
    }
}
