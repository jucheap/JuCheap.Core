using System.Collections.Generic;
using System.Linq;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Edit(string id)
        {
            var model = _roleService.Find(id);
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
        public IActionResult AuthenMenuDatas()
        {
            var list = _menuService.GetTrees();
            return Json(list);
        }

        /// <summary>
        /// 获取角色树
        /// </summary>
        /// <returns></returns>
        public IActionResult AuthenRoleDatas()
        {
            var list = _roleService.GetTrees();
            return Json(list);
        }

        /// <summary>
        /// 获取角色下的菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult AuthenRoleMenus(string id)
        {
            var list = _menuService.GetMenusByRoleId(id);
            var menuIds = list?.Select(item => item.Id);
            return Json(menuIds);
        }

        /// <summary>
        /// 获取角色下的菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetRoleMenus([FromBody]List<RoleMenuDto> datas)
        {
            var result = new JsonResultModel<bool>();
            if (datas.AnyOne())
            {
                result.flag = _roleService.SetRoleMenus(datas);
            }
            else
            {
                
            }
            return Json(result);
        }

        /// <summary>
        /// 清空该角色下的所有权限
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ClearRoleMenus(string id)
        {
            var result = new JsonResultModel<bool>
            {
                flag = _roleService.ClearRoleMenus(id)
            };
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(RoleDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Add(dto);
                if (result.IsNotBlank())
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
        public IActionResult Edit(RoleDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var result = _roleService.Update(dto);
            if (result)
                return RedirectToAction("Index");
            return View(dto);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public JsonResult Delete([FromBody]IEnumerable<string> ids)
        {
            var result = new JsonResultModel<bool>();
            if (ids.AnyOne())
            {
                result.flag = _roleService.Delete(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public JsonResult GetListWithPager(RoleFilters filters)
        {
            var result = _roleService.Search(filters);
            return Json(result);
        }
    }
}