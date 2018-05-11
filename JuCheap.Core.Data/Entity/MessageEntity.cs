using System.Collections.Generic;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 站内信
    /// </summary>
    public partial class MessageEntity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 已读人数
        /// </summary>
        public int ReadedNumber { get; set; }

        /// <summary>
        /// 总接收人数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 消息接收人
        /// </summary>
        public virtual IList<MessageReceiverEntity> MessageReceivers { get; set; }
    }
}
