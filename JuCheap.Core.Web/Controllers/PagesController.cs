using JuCheap.Core.Infrastructure.Attributes;
using JuCheap.Core.Infrastructure.Menu;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    public class PagesController : Controller
    {

        [Menu(Id = Menu.PageButtonId, ParentId = Menu.PagesId, Name = "按钮", Order = "1")]
        public IActionResult Buttons()
        {
            return View();
        }

        [Menu(Id = Menu.PageFontId, ParentId = Menu.PagesId, Name = "字体", Order = "2")]
        public IActionResult FontAwesome()
        {
            return View();
        }

        [Menu(Id = Menu.PageFormId, ParentId = Menu.PagesId, Name = "表单", Order = "3")]
        public IActionResult Form()
        {
            return View();
        }

        [Menu(Id = Menu.PageFormAdvanceId, ParentId = Menu.PagesId, Name = "高级表单", Order = "4")]
        public IActionResult FormAdvance()
        {
            return View();
        }

        [Menu(Id = Menu.PageTableId, ParentId = Menu.PagesId, Name = "表格", Order = "5")]
        public IActionResult Tables()
        {
            return View();
        }

        [Menu(Id = Menu.PageTabId, ParentId = Menu.PagesId, Name = "选项卡", Order = "6")]
        public IActionResult Tabs()
        {
            return View();
        }
    }
}