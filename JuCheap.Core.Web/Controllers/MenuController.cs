using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using System;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuSvc)
        {
            _menuService = menuSvc;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View(new MenuDto());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _menuService.FindAsync(id);
            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(MenuDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuService.AddAsync(dto);
                if (result != Guid.Empty)
                    return RedirectToAction("Index");
            }
            return View(dto);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(MenuDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuService.UpdateAsync(dto);
                if (result)
                    return RedirectToAction("Index");
            }
            return View(dto);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([FromBody]IEnumerable<Guid> ids)
        {
            var result = new JsonResultModel<bool>();
            if (ids.AnyOne())
            {
                result.flag = await _menuService.DeleteAsync(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetListWithPager(MenuFilters filters)
        {
            var result = await _menuService.SearchAsync(filters);
            return Json(result);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetListWithKeywords(MenuFilters filters)
        {
            filters.page = 1;
            filters.rows = 10;
            filters.ExcludeType = MenuType.Button; 
            var result = await _menuService.SearchAsync(filters);
            return Json(new {value = result.rows});
        }
    }
}