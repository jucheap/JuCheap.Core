namespace JuCheap.Core.Models
{
    /// <summary>
    /// 当前登陆用户数据传输对象
    /// </summary>
    public class CurrentUserDto
    {
        /// <summary>
        /// 当前用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginName { get; set; }
    }
}
