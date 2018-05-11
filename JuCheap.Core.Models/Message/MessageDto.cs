using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuCheap.Core.Models
{
    public class MessageDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "站内信标题")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(50, ErrorMessage = Message.MaxLength)]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "站内信内容")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(500, ErrorMessage = Message.MaxLength)]
        public string Contents { get; set; }

        /// <summary>
        /// 是否是发送给所有人
        /// </summary>
        [Display(Name = "发送给所有人")]
        public bool IsToAll { get; set; }

        /// <summary>
        /// 接收人Id集合
        /// </summary>
        [Display(Name = "用户")]
        public IList<string> UserIds { get; set; }
    }
}
