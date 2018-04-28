using System;
using System.Linq;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 日志
    /// </summary>
    [Authorize]
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        private readonly ISiteViewService _siteViewService;

        public LogController(ILogService logService, ISiteViewService siteViewService)
        {
            _logService = logService;
            _siteViewService = siteViewService;
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <returns></returns>
            public IActionResult Logins()
        {
            return View();
        }

        /// <summary>
        /// 访问记录
        /// </summary>
        /// <returns></returns>
        public IActionResult Visits()
        {
            return View();
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> LoginsList(LogFilters filters)
        {
            var result = await _logService.SearchLoginLogsAsync(filters);
            return Json(result);
        }

        /// <summary>
        /// 访问记录
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> VisitsList(LogFilters filters)
        {
            var result = await _logService.SearchVisitLogsAsync(filters);
            return Json(result);
        }

        /// <summary>
        /// 统计图表
        /// </summary>
        /// <returns></returns>
        public IActionResult Charts()
        {
            return View();
        }

        /// <summary>
        /// 获取统计数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> ChartsDatas()
        {
            var end = DateTime.Now.Date.AddDays(-1);
            var start = DateTime.Now.Date.AddDays(-7);
            var list = await _siteViewService.GetSiteViews(start, end);
            var result = new
            {
                categoryDatas = list.Select(x => x.Day.ToString("yyyy-MM-dd")),
                datas = list.Select(x => x.Number),
                pieDatas = list.Select(x => new { value = x.Number, name = x.Day.ToString("yyyy-MM-dd") })
            };
            return Json(result);
        }
    }
}