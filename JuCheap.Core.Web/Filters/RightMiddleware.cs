using System.Linq;
using System.Net;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Web;
using JuCheap.Core.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class RightFilter : ActionFilterAttribute
    {
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isIgnored = filterContext.ActionDescriptor.FilterDescriptors.Any(f => f.Filter is IgnoreRightFilter);
            if (!isIgnored)
            {
                var userService = filterContext.HttpContext.RequestServices.GetService<IUserService>();
                var context = filterContext.HttpContext;
                var identity = context.User.Identity;
                var routeData = filterContext.RouteData.Values;
                var controller = routeData["controller"];
                var action = routeData["action"];
                var url = string.Format("/{0}/{1}", controller, action);
                var hasRight = userService.HasRight(identity.GetLoginUserId(), url);

                if (hasRight) return;
                var isAjax = context.Request.Headers["Accept"].ToString().ToLower().Contains("application/json");
                if (isAjax)
                {
                    var data = new
                    {
                        flag = false,
                        code = (int)HttpStatusCode.Unauthorized,
                        msg = "您没有权限！"
                    };
                    filterContext.Result = new JsonResult(data);
                }
                else
                {
                    var view = new ViewResult
                    {
                        ViewName = "~/Views/Shared/NoRight.cshtml",
                    };
                    filterContext.Result = view;
                }
            }
        }
    }
}