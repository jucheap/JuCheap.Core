using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using System;
using JuCheap.Core.Infrastructure.Attributes;
using JuCheap.Core.Infrastructure.Menu;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 任务模板
    /// </summary>
    [Authorize]
    public class TaskTemplateController : BaseController
    {
        private readonly ITaskTemplateService _taskTemplateService;

        public TaskTemplateController(ITaskTemplateService taskTemplateService)
        {
            _taskTemplateService = taskTemplateService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.TaskTemplatePageId, ParentId = Menu.SystemId, Name = "任务模板管理", Order = "9")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.TaskTemplateAddId, ParentId = Menu.TaskTemplatePageId, Name = "添加任务模板", Order = "1")]
        public async Task<IActionResult> Add(string id)
        {
            var template = (await _taskTemplateService.GetTemplateAsync(id)) ?? new TaskTemplateDto();
            
            return View(template);
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TaskTemplateDto template)
        {
            var templateId = await _taskTemplateService.CreateAsync(template, GetCurrentUser());
            if (templateId.IsNotBlank())
                return RedirectToAction("AddForm", new { id = templateId });
            return View(template);
        }

        /// <summary>
        /// 添加模板表单
        /// </summary>
        /// <param name="id">模板Id</param>
        public IActionResult AddForm(string id)
        {
            return View("AddForm", id);
        }

        /// <summary>
        /// 添加模板表单
        /// </summary>
        /// <param name="id">模板Id</param>
        [HttpGet]
        public async Task<IActionResult> GetForms(string id)
        {
            var forms = await _taskTemplateService.GetFormsAsync(id);

            return Json(forms);
        }

        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="id">模板Id</param>
        [HttpPost]
        public async Task<IActionResult> DeleteForm(string id)
        {
            await _taskTemplateService.DeleteFormAsync(id);

            return Json(true);
        }

        /// <summary>
        /// 添加模板表单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddForm(TaskTemplateFormDto form)
        {
            await _taskTemplateService.CreateFormsAsync(form, GetCurrentUser());
            return RedirectToAction("AddForm", new { id = form.TemplateId });
        }

        /// <summary>
        /// 添加模板步骤
        /// </summary>
        /// <param name="id">模板Id</param>
        public IActionResult AddStep(string id)
        {
            var model = new TaskTemplateStepDto
            {
                TemplateId = id
            };
            return View(model);
        }

        /// <summary>
        /// 添加模板步骤
        /// </summary>
        /// <param name="id">模板Id</param>
        [HttpGet]
        public async Task<IActionResult> AddStepDatas(string id, bool isAdd)
        {
            var steps = new List<TaskTemplateStepDto>();
            if (!isAdd)
            {
                var list = await _taskTemplateService.GetStepsAsync(id);
                if (list.AnyOne())
                {
                    steps.AddRange(list);
                }
            }

            return Json(steps);
        }

        /// <summary>
        /// 添加模板步骤
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStep(TaskTemplateStepDto step)
        {
            await _taskTemplateService.CreateStepsAsync(step, GetCurrentUser());
            return RedirectToAction("AddStep", new { id = step.TemplateId });
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetListWithPager(BaseFilter filters)
        {
            var result = await _taskTemplateService.SearchAsync(filters);
            return Json(result);
        }
    }
}