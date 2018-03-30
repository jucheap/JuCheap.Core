using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("search")]
        public async Task<List<TreeDto>> GetMenus()
        {
            var menus = await _menuService.GetTreesAsync();
            return menus;
        }

        [HttpGet]
        [Route("my")]
        [Authorize]
        public async Task<List<MenuDto>> GetUserMenu()
        {
            var menus = await _menuService.GetMyMenusAsync(User.Claims.FirstOrDefault(x => x.Type == "sub").Value);
            return menus;
        }
    }
}
