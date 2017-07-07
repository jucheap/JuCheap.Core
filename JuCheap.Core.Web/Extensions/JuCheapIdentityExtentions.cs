using System;
using System.Security.Claims;
using System.Security.Principal;

namespace JuCheap.Core.Web
{
    public static class JuCheapIdentityExtentions
    {
        public static string GetMemberId(this IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");
            var identity1 = identity as ClaimsIdentity;
            if (identity1 != null)
                return identity1.FindFirst("sub").Value.ToString();
            return string.Empty;
        }
    }
}