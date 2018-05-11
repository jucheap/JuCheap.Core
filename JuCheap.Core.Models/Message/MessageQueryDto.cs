
using System;

namespace JuCheap.Core.Models
{
    public class MessageQueryDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 已读人数
        /// </summary>
        public int ReadedNumber { get; set; }

        /// <summary>
        /// 总接收人数
        /// </summary>
        public int Total { get; set; }
    }
}
