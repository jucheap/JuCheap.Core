using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JuChea.Core.Web.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        public ActionResult Index()
        {
            _areaService.UpdatePathCodes();
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id">父页面ID</param>
        /// <returns></returns>
        public async Task<ActionResult> Add(string id)
        {
            var parentDepartment = await _areaService.Find(id);
            var model = new AreaDto
            {
                ParentId = id
            };
            if (parentDepartment != null)
            {
                model.ParentName = parentDepartment.Name;
            }
            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Add(AreaDto model)
        {
            if (ModelState.IsValid)
            {
                var id = await _areaService.Add(model);
                if (id.IsNotBlank())
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
        /// <returns></returns>
        public async Task<ActionResult> Edit(string id)
        {
            var model = await _areaService.Find(id);
            return View(model);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(AreaDto model)
        {
            if (ModelState.IsValid)
            {
                var success = await _areaService.Update(model);
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
        public async Task<ActionResult> Delete(string id)
        {
            var success = await _areaService.Delete(new List<string> { id });
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
            var pages = await _areaService.FindByParentId(model.id);
            return Json(pages);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<ActionResult> Exists(string Id, string Name)
        {
            var exists = await _areaService.IsExists(Id, Name);
            return Json(!exists);
        }

    }
}