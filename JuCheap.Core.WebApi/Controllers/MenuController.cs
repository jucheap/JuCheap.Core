using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.WebApi.Controllers
{
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 不需要登录认证就能访问的api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public async Task<List<TreeDto>> GetMenus()
        {
            var menus = await _menuService.GetTreesAsync();
            return menus;
        }
        /// <summary>
        /// 需要登录后才能访问的api地址，获取登录用户的授权菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("my")]
        [Authorize]
        public async Task<List<MenuDto>> GetUserMenu()
        {
            var userId = User.GetLoginUserId();
            var menus = await _menuService.GetMyMenusAsync(userId);
            return menus;
        }
    }
}
