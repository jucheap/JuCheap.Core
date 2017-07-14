using System.Security.Claims;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;
using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using log4net;
using JuCheap.Core.Infrastructure.Utilities;

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
        private readonly IHostingEnvironment _hostEnvironment;
        private ILog log = LogManager.GetLogger(Constants.Log4net.RepositoryName, Constants.Log4net.LoggerName);

        public HomeController(IUserService userSvr, IMenuService menuService,IHostingEnvironment hostEnvironment)
        {
            _userService = userSvr;
            _menuService = menuService;
            _hostEnvironment = hostEnvironment;
            //log.Error("home ctor error");
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var myMenus = await _menuService.GetMyMenusAsync(User.Identity.GetLoginUserId());
            ViewBag.Menus = myMenus;
            return View();
        }

        /// <summary>
        /// 欢迎页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
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
        public async Task<IActionResult> Login(LoginDto model)
        {

            if (!ModelState.IsValid) return View(model);

            var connection = Request.HttpContext.Features.Get<IHttpConnectionFeature>();

            //var localIpAddress = connection.LocalIpAddress;    //本地IP地址
            //var localPort = connection.LocalPort;              //本地IP端口
            var remoteIpAddress = connection.RemoteIpAddress;  //远程IP地址
            //var remotePort = connection.RemotePort;            //本地IP端口

            model.LoginIP = remoteIpAddress.ToString();
            var loginDto = await _userService.LoginAsync(model);
            if (loginDto.LoginSuccess)
            {
                var authenType = CookieAuthenticationDefaults.AuthenticationScheme;
                var identity = new ClaimsIdentity(authenType);
                identity.AddClaim(new Claim(ClaimTypes.Name, loginDto.User.LoginName));
                identity.AddClaim(new Claim("LoginUserId", loginDto.User.Id.ToString()));
                var properties = new AuthenticationProperties() { IsPersistent = true };
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.Authentication.SignInAsync(authenType, principal, properties);
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
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 错误页面
        /// </summary>
        /// <returns></returns>
        [IgnoreRightFilter]
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// 上传文件Demo
        /// </summary>
        /// <param name="fileinput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile fileinput)
        {
            // 文件大小
            long size = 0;
            // 原文件名（包括路径）
            var filename = ContentDispositionHeaderValue.Parse(fileinput.ContentDisposition).FileName;
            // 扩展名
            var extName = filename.Substring(filename.LastIndexOf('.')).Replace("\"", "");
            // 新文件名
            string shortfilename = $"{Guid.NewGuid()}{extName}";
            // 新文件名（包括路径）
            filename = _hostEnvironment.WebRootPath + @"\upload\" + shortfilename;
            // 设置文件大小
            size += fileinput.Length;
            // 创建新文件
            using (FileStream fs = System.IO.File.Create(filename))
            {
                // 复制文件
                await fileinput.CopyToAsync(fs);
                // 清空缓冲区数据
                await fs.FlushAsync();
            }
            return View();
        }
    }
}