using JuCheap.Core.Infrastructure.Attributes;
using JuCheap.Core.Infrastructure.Menu;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    public class WorkPositionController : BaseController
    {
        /// <summary>
        /// 工位首页
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.WorkPositionPage, ParentId = Menu.WorkPositionId, Name = "工位列表", Order = "1")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 工位详情
        /// </summary>
        /// <returns></returns>
        [Menu(Id = Menu.WorkPositionDetail, ParentId = Menu.WorkPositionPage, Name = "工位详情", Order = "1")]
        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}