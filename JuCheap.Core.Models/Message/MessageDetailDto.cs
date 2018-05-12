using System;

namespace JuCheap.Core.Models
{
    public class MessageDetailDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsReaded { get; set; }
        /// <summary>
        /// 查收日期
        /// </summary>
        public DateTime? ReadDate { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
