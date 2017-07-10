// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;

namespace JuCheap.Core.Web
{
    [SecurityHeaders]
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEventService _events;
        private readonly AccountService _account;
        private readonly IUserService _userService;

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IHttpContextAccessor httpContextAccessor,
            IEventService events,
            IUserService userService)
        {
            _interaction = interaction;
            _events = events;
            _account = new AccountService(interaction, httpContextAccessor, clientStore);
            _userService = userService;
        }

        /// <summary>
        /// Show login page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await _account.BuildLoginViewModelAsync(returnUrl);
            return View(vm);
        }

        /// <summary>
        /// Show reg page
        /// </summary>
        [HttpGet]
        public IActionResult Reg()
        {
            return View();
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Reg(UserAddDto model)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddAsync(model);
                return Redirect("/");
            }
            return View();
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                var userDto = await _userService.LoginAsync(model.Username, model.Password);
                if (userDto != null)
                {
                    AuthenticationProperties props = null;
                    // only set explicit expiration here if persistent. 
                    // otherwise we reply upon expiration configured in cookie middleware.
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    // issue authentication cookie with subject ID and username
                    await _events.RaiseAsync(new UserLoginSuccessEvent(userDto.LoginName, userDto.Id.ToString(), userDto.LoginName));
                    await HttpContext.Authentication.SignInAsync(userDto.Id.ToString(), userDto.LoginName, props);

                    // make sure the returnUrl is still valid, and if yes - redirect back to authorize endpoint or a local page
                    if (_interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("~/");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));

                ModelState.AddModelError("", AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await _account.BuildLoginViewModelAsync(model);
            return View(vm);
        }

        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await _account.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // no need to show prompt
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var vm = await _account.BuildLoggedOutViewModelAsync(model.LogoutId);
            if (vm.TriggerExternalSignout)
            {
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });
                try
                {
                    // hack: try/catch to handle social providers that throw
                    await HttpContext.Authentication.SignOutAsync(vm.ExternalAuthenticationScheme,
                        new AuthenticationProperties { RedirectUri = url });
                }
                catch (NotSupportedException) // this is for the external providers that don't have signout
                {
                }
                catch (InvalidOperationException) // this is for Windows/Negotiate
                {
                }
            }

            // delete local authentication cookie
            await HttpContext.Authentication.SignOutAsync();

            var user = await HttpContext.GetIdentityServerUserAsync();
            if (user != null)
            {
                await _events.RaiseAsync(new UserLogoutSuccessEvent(user.GetSubjectId(), user.GetName()));
            }

            return View("LoggedOut", vm);
        }
    }
}