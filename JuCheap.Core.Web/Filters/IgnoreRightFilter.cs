using Microsoft.AspNetCore.Mvc.Filters;

namespace JuCheap.Core.Web.Filters
{
    /// <summary>
    /// 忽略权限验证，不需要做权限验证的，就加上这个Attribute(Action和Controller都可以加)
    /// </summary>
    public class IgnoreRightFilter : ActionFilterAttribute
    {
    }
}