using System.Collections.Generic;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 任务流模板实体
    /// </summary>
    public class TaskTemplateEntity : BaseEntity
    {
        /// <summary>
        /// 任务流模板名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 表单集合
        /// </summary>
        public virtual IList<TaskTemplateFormEntity> Forms { get; set; }
        /// <summary>
        /// 步骤集合
        /// </summary>
        public virtual IList<TaskTemplateStepEntity> Steps { get; set; }
    }
}
