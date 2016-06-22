using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 权限验证管道
    /// </summary>
    public class RightMiddleware
    {
        private readonly RequestDelegate _request;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="requestDelegate"></param>
        public RightMiddleware(RequestDelegate requestDelegate)
        {
            _request = requestDelegate;
        }

        /// <summary>
        /// 处理事件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var userService = context.RequestServices.GetRequiredService<IUserService>();
            var identity = context.User.Identity;
            var url = context.Request.Path;
            var hasRight = await userService.HasRightAsync(identity.GetLoginUserId(), url);

            if (!hasRight)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                //还需要处理ajax和一般http请求，未完成
            }

            await _request(context);
        }
    }
}