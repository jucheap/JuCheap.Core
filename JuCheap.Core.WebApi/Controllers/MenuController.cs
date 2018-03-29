using System.Collections.Generic;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
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
    }
}
