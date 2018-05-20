using System;
using System.ComponentModel.DataAnnotations;

namespace JuCheap.Core.Models
{
    public class TaskTemplateDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "模板名称")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(100, ErrorMessage = Message.MaxLength)]
        public string Name { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDateTime { get; set; }
    }
}
