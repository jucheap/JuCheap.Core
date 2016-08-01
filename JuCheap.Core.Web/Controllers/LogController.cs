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

        public LogController(ILogService logService)
        {
            _logService = logService;
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
    }
}