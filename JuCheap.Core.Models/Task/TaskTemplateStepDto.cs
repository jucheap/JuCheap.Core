using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JuCheap.Core.Models
{
    public class TaskTemplateStepDto
    {
        /// <summary>
        /// 所属模板Id
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 步骤名称
        /// </summary>
        [Display(Name = "步骤名称")]
        [Required(ErrorMessage = Message.Required)]
        [MaxLength(20, ErrorMessage = Message.MaxLength)]
        public string StepName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Required(ErrorMessage = Message.Required)]
        public int Order { get; set; }

        /// <summary>
        /// 操作集合
        /// </summary>
        public IList<TaskTemplateStepOperateDto> Operates { get; set; }
    }
}
