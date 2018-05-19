using System.Collections.Generic;

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
        public string StepName { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 操作集合
        /// </summary>
        public IList<TaskTemplateStepOperateDto> Operates { get; set; }
    }
}
