using System.ComponentModel;

namespace JuCheap.Core.Infrastructure.Enums
{
    /// <summary>
    /// 模板完成步骤
    /// </summary>
    public enum TaskTemplateStep
    {
        /// <summary>
        /// 添加模板
        /// </summary>
        [Description("添加模板")]
        Save = 0,
        /// <summary>
        /// 表单设计
        /// </summary>
        [Description("表单设计")]
        DesignForms = 1,
        /// <summary>
        /// 添加步骤
        /// </summary>
        [Description("添加步骤")]
        DesignSteps = 2,
        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Finished = 99
    }
}
