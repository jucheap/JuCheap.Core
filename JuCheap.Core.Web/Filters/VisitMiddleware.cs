using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 访问管道
    /// </summary>
    public class VisitMiddleware
    {
        readonly RequestDelegate _next;

        public VisitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            #region 记录访问记录

            try
            {
                var userService = context.RequestServices.GetService<IUserService>();

                var connection = context.Features.Get<IHttpConnectionFeature>();
                var user = context.User;
                var isLogined = user != null && user.Identity != null && user.Identity.IsAuthenticated;
                var visit = new VisitDto
                {
                    Ip = connection.RemoteIpAddress.ToString(),
                    LoginName = isLogined ? user.Identity.Name : string.Empty,
                    Url = context.Request.Path,
                    UserId = isLogined ? user.Identity.GetLoginUserId().ToString() : "0"
                };
                await userService.VisitAsync(visit);
            }
            catch
            {
                //eat exception
            }

            #endregion
        }
    }
}
