using System.Net;
using System.Reflection;
using JuCheap.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class RightFilter : IActionFilter
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var isIgnored = filterContext.ActionDescriptor.IsDefined(typeof(IgnoreRightFilter), true) ||
            //    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(IgnoreRightFilter), true);
            //if (!isIgnored)
            //{
            //    var userService = DependencyResolver.Current.GetService<IUserService>();
            //    var context = filterContext.HttpContext;
            //    var identity = context.User.Identity;
            //    var routeData = filterContext.RouteData.Values;
            //    var controller = routeData["controller"];
            //    var action = routeData["action"];
            //    var url = string.Format("/{0}/{1}", controller, action);
            //    var hasRight = userService.HasRight(identity.GetLoginUserId(), url);

            //    if (hasRight) return;

            //    if (context.Request.IsAjaxRequest())
            //    {
            //        var data = new
            //        {
            //            flag = false,
            //            code = (int)HttpStatusCode.Unauthorized,
            //            msg = "您没有权限！"
            //        };
            //        filterContext.Result = new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //    }
            //    else
            //    {
            //        var view = new ViewResult
            //        {
            //            ViewName = "~/Views/Shared/NoRight.cshtml",
            //        };
            //        filterContext.Result = view;
            //    }
            //}
        }

        /// <summary>
        /// OnActionExecuted
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //to do
        }
    }
}