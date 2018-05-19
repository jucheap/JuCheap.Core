using System.Collections.Generic;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 任务流模板步骤实体
    /// </summary>
    public class TaskTemplateStepEntity : BaseEntity
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
        /// 步骤顺序(升序排列)
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 所属模板
        /// </summary>
        public virtual TaskTemplateEntity Template { get; set; }
        /// <summary>
        /// 操作集合
        /// </summary>
        public virtual IList<TaskTemplateStepOperateEntity> Operates { get; set; }
    }
}
