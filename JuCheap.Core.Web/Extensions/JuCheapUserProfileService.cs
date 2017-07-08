using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using JuCheap.Core.Interfaces;
using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace JuCheap.Core.Web.Extensions
{
    /// <summary>
    /// Profile service for test users
    /// </summary>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class JuCheapUserProfileService : IProfileService
    {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;
        
        /// <summary>
        /// The users
        /// </summary>
        protected readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="JuCheapUserProfileService"/> class.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="logger">The logger.</param>
        public JuCheapUserProfileService(IUserService userService, ILogger<JuCheapUserProfileService> logger)
        {
            _userService = userService;
            Logger = logger;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userService.FindAsync(Guid.Parse(context.Subject.GetSubjectId()));
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("name",user.LoginName)
                };
                context.IssuedClaims.AddRange(claims);
            }
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = Guid.Parse(context.Subject.GetSubjectId());
            var user = await _userService.FindAsync(userId);
            context.IsActive = user != null;
        }
    }
}