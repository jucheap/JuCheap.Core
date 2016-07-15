using System;

namespace JuCheap.Core.Models
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class LoginLogDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 登录结果信息
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
