using AutoMapper;
using AutoMapper.QueryableExtensions;
using JuCheap.Core.Data;
using JuCheap.Core.Data.Entity;
using JuCheap.Core.Infrastructure;
using JuCheap.Core.Infrastructure.Enums;
using JuCheap.Core.Infrastructure.Exceptions;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IConfigurationProvider _configurationProvider;

        public TaskTemplateService(JuCheapContext context, IMapper mapper, IConfigurationProvider configurationProvider)
        {
            _context = context;
            _mapper = mapper;
            _configurationProvider = configurationProvider;
        }

        /// <summary>
        /// 创建任务流模板
        /// </summary>
        public async Task<string> CreateAsync(TaskTemplateDto templateDto, CurrentUserDto user)
        {
            if (templateDto.Name.IsBlank())
            {
                throw new BusinessException("模板名称不能为空");
            }
            if (templateDto.Id.IsNotBlank())
            {
                var template = await _context.TaskTemplates.FindAsync(templateDto.Id);
                if (template != null)
                {
                    template.Name = templateDto.Name;
                }
            }
            else
            {
                var template = new TaskTemplateEntity { Name = templateDto.Name };
                template.CreateBy(user.UserId);
                template.SetStep(TaskTemplateStep.Save);
                await _context.AddAsync(template);
            }
            
           
            await _context.SaveChangesAsync();
            return templateDto.Id;
        }

        /// <summary>
        /// 创建表单信息
        /// </summary>
        public async Task CreateFormsAsync(TaskTemplateFormDto form, CurrentUserDto user)
        {
            var template = await _context.TaskTemplates.FirstOrDefaultAsync(x => x.Id == form.TemplateId);
            template.SetStep(TaskTemplateStep.DesignForms);
            if (form.Id.IsNotBlank())
            {
                var templateForm = await _context.TaskTemplateForms.FirstOrDefaultAsync(x => x.Id == form.Id);
                if (templateForm != null)
                {
                    _mapper.Map(form, templateForm);
                }
            }
            else
            {
                var formEntity = _mapper.Map<TaskTemplateFormEntity>(form);
                formEntity.CreateBy(user.UserId);
                await _context.TaskTemplateForms.AddAsync(formEntity);
            }
            
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        public async Task<bool> DeleteFormAsync(string formId)
        {
            var form = await _context.TaskTemplateForms.FirstOrDefaultAsync(x => x.Id == formId);
            if (form != null)
            {
                _context.Remove(form);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        /// <summary>
        /// 创建步骤操作信息
        /// </summary>
        public async Task CreateStepsAsync(TaskTemplateStepDto stepDto, CurrentUserDto user)
        {
            var template = await _context.TaskTemplates.FirstOrDefaultAsync(x => x.Id == stepDto.TemplateId);
            template.SetStep(TaskTemplateStep.DesignSteps);
            //删除以前的数据
            var operates = await _context.TaskTemplateStepOperates.Where(x => x.StepId == stepDto.Id).ToListAsync();
            _context.TaskTemplateStepOperates.RemoveRange(operates);
            var oldStep = await _context.TaskTemplateSteps.FirstOrDefaultAsync(x => x.Id == stepDto.Id);
            _context.TaskTemplateSteps.Remove(oldStep);
            var step = _mapper.Map<TaskTemplateStepEntity>(stepDto);
            step.Operates = step.Operates.Where(o => o.Name.IsNotBlank()).ToList();
            step.CreateBy(user.UserId);
            step.Operates.ForEach(m =>
            {
                m.StepId = step.Id;
                m.CreateBy(user.UserId);
            });
            await _context.TaskTemplateSteps.AddAsync(step);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 获取任务模板列表
        /// </summary>
        public async Task<PagedResult<TaskTemplateDto>> SearchAsync(BaseFilter filters)
        {
            var query = _context.TaskTemplates
                .WhereIf(filters.keywords.IsNotBlank(), x => x.Name.Contains(filters.keywords));

            return await query.OrderByDescending(x => x.CreateDateTime)
                .ProjectTo<TaskTemplateDto>(_configurationProvider)
                .PagingAsync(filters.page, filters.rows);
        }

        /// <summary>
        /// 获取任务模板的信息
        /// </summary>
        public async Task<TaskTemplateDto> GetTemplateAsync(string templateId)
        {
            if (templateId.IsBlank())
            {
                return null;
            }
            var query = _context.TaskTemplates.Where(x => x.Id == templateId);
            return await query.ProjectTo<TaskTemplateDto>(_configurationProvider).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取模板的表单信息
        /// </summary>
        public async Task<IList<TaskTemplateFormDto>> GetFormsAsync(string templateId)
        {
            var query = _context.TaskTemplateForms
                .OrderBy(x => x.Order)
                .Where(x => x.TemplateId == templateId);
            return await query.ProjectTo<TaskTemplateFormDto>(_configurationProvider).ToListAsync();
        }

        /// <summary>
        /// 获取模板的步骤信息
        /// </summary>
        public async Task<IList<TaskTemplateStepDto>> GetStepsAsync(string templateId)
        {
            var query = _context.TaskTemplateSteps
                .Include(x => x.Operates)
                .OrderBy(x => x.Order)
                .Where(x => x.TemplateId == templateId);
            return await query.ProjectTo<TaskTemplateStepDto>(_configurationProvider).ToListAsync();
        }
    }
}
