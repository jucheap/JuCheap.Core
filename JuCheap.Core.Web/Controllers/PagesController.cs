using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [IgnoreRightFilter]
    [Route("pages")]
    public class PagesController : Controller
    { 
        [Route("{id}")]
        public IActionResult Index(string id)
        {
            return View(id);
        }
    }
}