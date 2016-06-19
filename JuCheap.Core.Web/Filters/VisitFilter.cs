using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 访问记录
    /// </summary>
    public class VisitFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 记录访问记录

            try
            {
                var userService = filterContext.HttpContext.Features.Get<IUserService>();
                
                var context = filterContext.HttpContext;
                var user = context.User;
                var isLogined = user != null && user.Identity != null && user.Identity.IsAuthenticated;
                var visit = new VisitDto
                {
                    IP = filterContext.HttpContext.Request.Headers["RemoteAddress"],
                    LoginName = isLogined ? user.Identity.Name : string.Empty,
                    Url = context.Request.Path,
                    UserId = isLogined ? user.Identity.GetLoginUserId().ToString() : "0"
                };
                userService.Visit(visit);
            }
            catch
            {
                //eat exception
            }

            #endregion
        }
    }
}