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
    public class TaskTemplateController : Controller
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
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加模板
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TaskTemplateDto template)
        {
            var templateId = await _taskTemplateService.Create(template.Name, null);
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
            return View();
        }

        /// <summary>
        /// 添加模板步骤
        /// </summary>
        /// <param name="id">模板Id</param>
        public IActionResult AddStep(string id)
        {
            return View();
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