// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using System.Collections.Generic;
using JuCheap.Core.Models;

namespace JuCheap.Core.Web
{
    [SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAppService _appService;

        public HomeController(IIdentityServerInteractionService interaction,
            IAppService appService)
        {
            _interaction = interaction;
            _appService = appService;
        }

        public async Task<IActionResult> Index()
        {
            List<AppDto> apps = new List<AppDto>();
            if (User.Identity.IsAuthenticated)
            {
                apps = await _appService.GetByUserId(User.Identity.GetMemberId());
            }            
            return View(apps);
        }

        /// <summary>
        /// Shows the error page
        /// </summary>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View("Error", vm);
        }
    }
}