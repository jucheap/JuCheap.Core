using System.Collections.Generic;
using AutoMapper;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Filters;
using JuCheap.Core.Services;
using JuCheap.Core.Web.Filters;
using JuCheap.Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using JuCheap.Core.Infrastructure.Extentions;

namespace JuCheap.Core.Web.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(IUserService userSvc, IRoleService roleSvc)
        {
            _userService = userSvc;
            _mapper = AutoMapperConfig.GetMapperConfiguration().CreateMapper();
            _roleService = roleSvc;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <returns></returns>
        public ActionResult Authen()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return View(new UserAddDto());
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var dto = _userService.Find(id);
            var model = _mapper.Map<UserDto, UserUpdateDto>(dto);
            return View(model);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(UserAddDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Add(dto);
                if (result > 0)
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
        public ActionResult Edit(UserUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Update(dto);
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
        public JsonResult Delete(IEnumerable<int> ids)
        {
            var result = new JsonResultModel<bool>();
            if (ids.AnyOne())
            {
                result.flag = _userService.Delete(ids);
            }
            return Json(result);
        }

        /// <summary>
        /// 搜索页面
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public JsonResult GetListWithPager(UserFilters filters)
        {
            var result = _userService.Search(filters);
            return Json(result);
        }

        /// <summary>
        /// 我的角色
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public JsonResult GetMyRoles(RoleFilters filters)
        {
            filters.UserId = User.Identity.GetLoginUserId();
            var result = _roleService.Search(filters);
            return Json(result);
        }

        /// <summary>
        /// 我尚未拥有的角色
        /// </summary>
        /// <param name="filters">查询参数</param>
        /// <returns></returns>
        [IgnoreRightFilter]
        public JsonResult GetNotMyRoles(RoleFilters filters)
        {
            filters.UserId = User.Identity.GetLoginUserId();
            filters.ExcludeMyRoles = true;
            var result = _roleService.Search(filters);
            return Json(result);
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GiveRight(int id)
        {
            var result = new JsonResultModel<bool>
            {
                flag = _userService.Give(User.Identity.GetLoginUserId(), id)
            };
            return Json(result);
        }

        /// <summary>
        /// 用户角色取消
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CancelRight(int id)
        {
            var result = new JsonResultModel<bool>
            {
                flag = _userService.Cancel(User.Identity.GetLoginUserId(), id)
            };
            return Json(result);
        }
    }
}