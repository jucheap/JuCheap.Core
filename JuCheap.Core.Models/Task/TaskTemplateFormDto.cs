using JuCheap.Core.Infrastructure.Enums;

namespace JuCheap.Core.Models
{
    public class TaskTemplateFormDto
    {
        /// <summary>
        /// 所属模板Id
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType ControlType { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }
    }
}
