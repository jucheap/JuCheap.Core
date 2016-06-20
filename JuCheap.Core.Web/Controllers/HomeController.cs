using System.Collections.Generic;
using System.Security.Claims;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;
using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [IgnoreRightFilter]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;

        public HomeController(IUserService userSvr, IMenuService menuService)
        {
            _userService = userSvr;
            _menuService = menuService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //var user = HttpContext.Features.Get<UserManager<ApplicationUser>>();
            //var userName = user.GetUserName(User);
            var myMenus = _menuService.GetMyMenus(User.Identity.GetLoginUserId());
            ViewBag.Menus = myMenus;
            return View();
        }

        /// <summary>
        /// 欢迎页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {

            var model = new LoginDto
            {
                ReturnUrl = Request.Query["ReturnUrl"],
                LoginName = "admin",
                Password = "qwaszx"
            };
            return View(model);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginDto model)
        {

            if (!ModelState.IsValid) return View(model);

            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口

            model.LoginIP = remoteIpAddress.ToString();
            var loginDto = _userService.Login(model);
            if (loginDto.LoginSuccess)
            {
                var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
                var identity = new ClaimsIdentity(authenType);
                identity.AddClaim(new Claim(ClaimTypes.Name, loginDto.User.LoginName));
                identity.AddClaim(new Claim("LoginUserId", loginDto.User.Id.ToString()));
                var properties = new AuthenticationProperties() {IsPersistent = true};
                var principal = new ClaimsPrincipal(identity);
                HttpContext.Authentication.SignInAsync(authenType, principal, properties);
                model.ReturnUrl = model.ReturnUrl.IsNotBlank() ? model.ReturnUrl : "/";
                return Redirect(model.ReturnUrl);
            }
            ModelState.AddModelError(loginDto.Result == LoginResult.AccountNotExists ? "LoginName" : "Password",
                loginDto.Message);
            return View(model);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}