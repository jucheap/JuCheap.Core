using JuCheap.Core.Infrastructure.Enums;
using System.Collections.Generic;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 任务流模板实体
    /// </summary>
    public partial class TaskTemplateEntity : BaseEntity
    {
        /// <summary>
        /// 任务流模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设计步骤
        /// </summary>
        public TaskTemplateStep Step { get; set; }
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
