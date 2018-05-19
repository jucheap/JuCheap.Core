using JuCheap.Core.Data.Enum;

namespace JuCheap.Core.Data.Entity
{
    /// <summary>
    /// 任务流模板步骤操作实体
    /// </summary>
    public class TaskTemplateStepOperateEntity : BaseEntity
    {
        /// <summary>
        /// 所属步骤Id
        /// </summary>
        public string StepId { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作方向(退回上一步，下一步，退回到发起人等)
        /// </summary>
        public OperateDirection OperateDirection { get; set; }
        /// <summary>
        /// 所属步骤
        /// </summary>
        public virtual TaskTemplateStepEntity Step { get; set; }
    }
}
