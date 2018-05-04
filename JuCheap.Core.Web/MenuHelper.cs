using JuCheap.Core.Infrastructure.Attributes;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Infrastructure.Menu;
using JuCheap.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JuCheap.Core.Web
{
    public class MenuHelper
    {
        /// <summary>
        /// 获取所有的菜单配置
        /// </summary>
        /// <returns></returns>
        public static List<MenuDto> GetMenues()
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
            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", string.Empty);

                var members = controller.GetMembers().Where(x => x.IsDefined(typeof(MenuAttribute)));

                foreach (var action in members)
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
                    if (menus.Any(x => x.Id == menu.Id))
                    {
                        throw new Exception($"已经存在相同的Id={menu.Id},Name={menu.Name}");
                    }
                    menus.Add(menu);
                }
            }
            return menus;
        }
    }
}
