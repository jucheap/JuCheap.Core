
using JuCheap.Core.Interfaces;
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
    [RightFilter]
    public class SystemController : Controller
    {
        private readonly IDatabaseInitService _databaseInitService;

        public SystemController(IDatabaseInitService databaseInitService)
        {
            _databaseInitService = databaseInitService;
        }

        /// <summary>
        /// 系统管理首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 重置路径码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ReloadPathCode()
        {
            var result = new JsonResultModel<bool>
            {
                flag = _databaseInitService.InitPathCode()
            };
            return Json(result);
        }
    }
}