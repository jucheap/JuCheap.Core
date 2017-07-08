using System;
using System.Security.Claims;
using System.Security.Principal;
using IdentityServer4.Extensions;

namespace JuCheap.Core.Web
{
    public static class JuCheapIdentityExtentions
    {
        public static Guid GetMemberId(this IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");
            var identity1 = identity as ClaimsIdentity;
            var id = identity1.GetSubjectId();
            var guidId = Guid.Empty;
            Guid.TryParse(id, out guidId);
            return guidId;
        }
    }
}