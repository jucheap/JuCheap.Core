using AutoMapper;
using JuCheap.Core.Data;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JuCheap.Core.Services.AppServices
{
    /// <summary>
    /// 任务流模板服务接口
    /// </summary>
    public class TaskTemplateService : ITaskTemplateService
    {
        private readonly JuCheapContext _context;
        private readonly IMapper _mapper;

        public TaskTemplateService(JuCheapContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 创建任务流模板
        /// </summary>
        public async Task<string> Create(string templateName, CurrentUserDto user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建表单信息
        /// </summary>
        public async Task CreateForms(IList<TaskTemplateFormDto> forms, CurrentUserDto user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建步骤操作信息
        /// </summary>
        public async Task CreateSteps(IList<TaskTemplateStepDto> steps, CurrentUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
