using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure.Attributes;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Menu;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Menu(Id = Menu.DepartmentPageId, ParentId = Menu.SystemId, Name = "部门管理", Order = "3")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id">父页面ID</param>
        /// <returns></returns>
        [Menu(Id = Menu.DepartmentAddId, ParentId = Menu.DepartmentPageId, Name = "添加部门", Order = "1")]
        public async Task<ActionResult> Add(string id)
        {
            var parentDepartment = await _departmentService.Find(id);
            var model = new DepartmentDto
            {
                ParentId = id
            };
            if (parentDepartment != null)
            {
                model.ParentName = parentDepartment.FullName;
            }
            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Add(DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var departmentId = await _departmentService.Add(model);
                if (departmentId.IsNotBlank())
                {
                    return PartialView("CloseLayerPartial");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns
        [Menu(Id = Menu.DepartmentEditId, ParentId = Menu.DepartmentPageId, Name = "编辑部门", Order = "2")]
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _departmentService.Find(id);
            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentDto model)
        {
            if (ModelState.IsValid)
            {
                var success = await _departmentService.Update(model);
                if (success)
                {
                    return PartialView("CloseLayerPartial");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpPost]
        [Menu(Id = Menu.DepartmentDeleteId, ParentId = Menu.DepartmentPageId, Name = "删除部门", Order = "3")]
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _departmentService.Delete(new List<string> { id });
            return Json(new { flag = success });
        }

        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, IgnoreRightFilter]
        public async Task<ActionResult> GetTrees(TreeFilter model)
        {
            var pages = await _departmentService.FindByParentId(model.id);
            return Json(pages);
        }

        /// <summary>
        /// 获取树
        /// </summary>
        /// <param name="q">查询关键字</param>
        /// <returns></returns>
        [HttpGet, IgnoreRightFilter]
        public async Task<ActionResult> GetDepartments(string q)
        {
            var filter = new BaseFilter
            {
                page = 1,
                rows = 1000,
                keywords = q
            };
            var result = await _departmentService.Search(filter);
            var depts = new
            {
                results = result.rows.Select(x => new {id = x.Id, text = x.FullName})
            };
            return Json(depts);
        }
    }
}