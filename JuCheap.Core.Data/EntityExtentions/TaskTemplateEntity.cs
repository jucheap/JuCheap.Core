using JuCheap.Core.Infrastructure.Enums;
namespace JuCheap.Core.Data.Entity
{
    public partial class TaskTemplateEntity
    {
        /// <summary>
        /// 设置完成步骤
        /// </summary>
        /// <param name="step"></param>
        public void SetStep(TaskTemplateStep step)
        {
            if (step > Step)
                Step = step;
        }
    }
}
