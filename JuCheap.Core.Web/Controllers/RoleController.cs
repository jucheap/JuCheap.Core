using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;
using System;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 用户角色
    /// </summary>
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMenuService _menuService;

        public RoleController(IRoleService roleSvc,IMenuService menuService)
        {
            _roleService = roleSvc;
            _menuService = menuService;
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
            return View(new RoleDto());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _roleService.FindAsync(id);
            return View(model);
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        public IActionResult Authen()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AuthenMenuDatas()
        {
            var list = await _menuService.GetTreesAsync();
            //默认tree节点是展开的
            list.ForEach(x => x.open = true);
            return Json(list);
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AuthenRoleDatas()
        {
            var list = await _roleService.GetTreesAsync();
            return Json(list);
        }

        /// <summary>
        /// 获取角色下的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AuthenRoleMenus(Guid id)
        {
            var list = await _menuService.GetMenusByRoleIdAsync(id);
            var menuIds = list?.Select(item => item.Id);
            return Json(menuIds);
        }

        /// <summary>
        /// 获取角色下的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetRoleMenus([FromBody]List<RoleMenuDto> datas)
        {
            var result = new JsonResultModel<bool>();
            if (datas.AnyOne())
            {
                result.flag = await _roleService.SetRoleMenusAsync(datas);
            }
            else
            {
                result.msg = "请选择权限";
            }
            return Json(result);
        }

        /// <summary>
        /// 清空该角色下的所有权限
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ClearRoleMenus(Guid id)
        {
            var result = new JsonResultModel<bool>
            {
                flag = await _roleService.ClearRoleMenusAsync(id)
            };
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(RoleDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.AddAsync(dto);
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
        public async Task<IActionResult> Edit(RoleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = await _roleService.UpdateAsync(dto);
            if (result)
                return RedirectToAction("Index");
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
                result.flag = await _roleService.DeleteAsync(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetListWithPager(RoleFilters filters)
        {
            var result = await _roleService.SearchAsync(filters);
            return Json(result);
        }
    }
}