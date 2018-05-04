using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JuCheap.Core.Infrastructure.Attributes;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Menu;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 系统管理
    /// </summary>
    [Authorize]
    public class SystemController : Controller
    {
        private readonly IDatabaseInitService _databaseInitService;
        private readonly IMenuService _menuService;

        public SystemController(IDatabaseInitService databaseInitService, IMenuService menuService)
        {
            _databaseInitService = databaseInitService;
            _menuService = menuService;
        }

        /// <summary>
        /// 系统管理首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.SystemConfigId, ParentId = Menu.SystemId, Name = "系统配置", Order = "1")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 重置路径码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Menu(Id = Menu.ResetPathCodeId, ParentId = Menu.SystemConfigId, Name = "重置路径码", Order = "1")]
        public async Task<IActionResult> ReloadPathCode()
        {
            var result = new JsonResultModel<bool>
            {
                flag = await _databaseInitService.InitPathCodeAsync()
            };
            return Json(result);
        }

        /// <summary>
        /// 初始化省市区数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Menu(Id = Menu.ResetAreaId, ParentId = Menu.SystemConfigId, Name = "重置路径码", Order = "2")]
        public async Task<JsonResult> ReInitAreas()
        {
            var result = new JsonResultModel<bool>
            {
                flag = await _databaseInitService.InitAreas()
            };
            return Json(result);
        }

        /// <summary>
        /// 重置系统所有菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ReInitMenues()  
        {
            var menus = new List<MenuDto>
            {
                new MenuDto{Id = Menu.System.Id,Name = Menu.System.Name,Icon = "fa fa-gear"},
                new MenuDto{Id = Menu.Logs.Id,Name = Menu.Logs.Name,Icon = "fa fa-bars"},
                new MenuDto{Id = Menu.Pages.Id,Name = Menu.Pages.Name,Icon = "fa fa-file-o"}
            };

            //获取所有的控制器
            var controllers =
                from type in typeof(Startup).Assembly.GetTypes()
                where type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                select type;
            //遍历所有控制器下面定义了MenuAttribute属性的Action
            foreach(var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);

                var members = controller.GetMembers().Where(x => x.IsDefined(typeof(MenuAttribute)));
                
                foreach(var action in members)
                {
                    var attr = action.GetCustomAttributes<MenuAttribute>().FirstOrDefault();
                    var actionName = action.Name;

                    var menu = new MenuDto
                    {
                        Id = attr.Id,
                        ParentId = attr.ParentId,
                        Name = attr.Name,
                        Order = attr.Order.ToInt(),
                        Url = $"/{controllerName}/{actionName}".ToLower()
                    };
                    if(menus.Any(x=>x.Id == menu.Id))
                    {
                        throw new Exception($"已经存在相同的Id={menu.Id},Name={menu.Name}");
                    }
                    menus.Add(menu);
                }
            }

            var succes = await _menuService.ReInitMenuesAsync(menus);
            var result = new JsonResultModel<bool> { flag = succes };
            return Json(result);
        }  
    }
}