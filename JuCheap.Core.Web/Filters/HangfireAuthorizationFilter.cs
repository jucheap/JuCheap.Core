using Hangfire.Dashboard;

namespace JuCheap.Core.Web
{
    /// <summary>
    /// Hangfire认证
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return httpContext.User.Identity.IsAuthenticated;
        }
    }
}