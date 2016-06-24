using Microsoft.AspNetCore.Mvc.Filters;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 忽略权限验证
    /// </summary>
    public class IgnoreRightFilter : ActionFilterAttribute
    {
    }
}