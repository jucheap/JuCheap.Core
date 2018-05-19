using JuCheap.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuCheap.Core.Interfaces
{
    /// <summary>
    /// 任务流模板接口
    /// </summary>
    public interface ITaskTemplateService
    {
        /// <summary>
        /// 创建任务流模板
        /// </summary>
        Task<string> Create(string templateName, CurrentUserDto user);

        /// <summary>
        /// 创建表单信息
        /// </summary>
        Task CreateForms(IList<TaskTemplateFormDto> forms, CurrentUserDto user);

        /// <summary>
        /// 创建步骤操作信息
        /// </summary>
        Task CreateSteps(IList<TaskTemplateStepDto> steps, CurrentUserDto user);
    }
}
