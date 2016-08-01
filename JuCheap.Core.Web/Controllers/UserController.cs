using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authorization;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(IUserService userSvc, IRoleService roleSvc, IMapper mapper)
        {
            _userService = userSvc;
            _roleService = roleSvc;
            _mapper = mapper;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <returns></returns>
        public IActionResult Authen()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View(new UserAddDto());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            var dto = await _userService.FindAsync(id);
            var model = _mapper.Map<UserDto, UserUpdateDto>(dto);
            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddAsync(dto);
                if (result.IsNotBlank())
                    return RedirectToAction("Index");
            }
            return View(dto);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateAsync(dto);
                if (result)
                    return RedirectToAction("Index");
            }
            return View(dto);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete([FromBody]IEnumerable<string> ids)
        {
            var result = new JsonResultModel<bool>();
            var enumerable = ids as IList<string> ?? ids.ToList();
            if (enumerable.AnyOne())
            {
                result.flag = await _userService.DeleteAsync(enumerable);
            }
            return Json(result);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetListWithPager(UserFilters filters)
        {
            var result = await _userService.SearchAsync(filters);
            return Json(result);
        }

        /// <summary>
        /// 我的角色
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetMyRoles(RoleFilters filters)
        {
            filters.UserId = User.Identity.GetLoginUserId();
            var result = await _roleService.SearchAsync(filters);
            return Json(result);
        }

        /// <summary>
        /// 我尚未拥有的角色
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public async Task<IActionResult> GetNotMyRoles(RoleFilters filters)
        {
            filters.UserId = User.Identity.GetLoginUserId();
            filters.ExcludeMyRoles = true;
            var result = await _roleService.SearchAsync(filters);
            return Json(result);
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GiveRight(string id)
        {
            var result = new JsonResultModel<bool>
            {
                flag = await _userService.GiveAsync(User.Identity.GetLoginUserId(), id)
            };
            return Json(result);
        }

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CancelRight(string id)
        {
            var result = new JsonResultModel<bool>
            {
                flag = await _userService.CancelAsync(User.Identity.GetLoginUserId(), id)
            };
            return Json(result);
        }
    }
}