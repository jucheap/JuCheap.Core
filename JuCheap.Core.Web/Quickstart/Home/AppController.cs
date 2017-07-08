using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JuCheap.Core.Models;
using System.Collections.Generic;
using JuCheap.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;

namespace JuCheap.Core.Web
{
    /// <summary>
    /// APP
    /// </summary>
    public class AppController : Controller
    {
        private IAppService _appService;
        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        // GET: App
        public async Task<IActionResult> List()
        {
            List<AppDto> list = null;
            var userId = User.Identity.GetMemberId();
            list = await _appService.GetByUserId(userId);
            return View(list);
        }

        // GET: App
        public IActionResult Add()
        {
            return View(new AppDto());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            return View(await GetModel(id));
        }

        /// <summary>
        /// 接入
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Join()
        {
            return View();
        }

        /// <summary>
        /// GetModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async Task<AppDto> GetModel(Guid id)
        {
            return await _appService.GetAsync(id);
        }

            /// <summary>
        /// Add
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AppDto app)
        {
            string errorMsg = string.Empty;
            if (ModelState.IsValid)
            {
                app.Enabled = true;
                app.UserId = User.Identity.GetMemberId();
                app.ClientId = Guid.NewGuid().ToString("N");
                var id = await _appService.AddOrUpdateAsync(app);
                if (id != Guid.Empty)
                {
                    return RedirectToAction("List");
                }
            }
            ViewBag.Error = errorMsg;
            return View(app);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(AppDto app)
        {
            string errorMsg = string.Empty;
            if (ModelState.IsValid)
            {
                await _appService.AddOrUpdateAsync(app);
                return RedirectToAction("List");
            }
            ViewBag.Error = errorMsg;
            return View(app);
        }
    }
}