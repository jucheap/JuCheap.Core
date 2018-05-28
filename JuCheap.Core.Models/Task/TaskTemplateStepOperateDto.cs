using JuCheap.Core.Infrastructure.Enums;

namespace JuCheap.Core.Models
{
    public class TaskTemplateStepOperateDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 所属步骤Id
        /// </summary>
        public string StepId { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作方向
        /// </summary>
        public OperateDirection OperateDirection { get; set; }


    }
}
