using System;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using JuCheap.Core.Interfaces;

namespace JuCheap.Core.WebApi
{
    public class ProfileService : IProfileService
    {
        //services
        private readonly IUserService _userService;

        public ProfileService(IUserService userService)
        {
            _userService = userService;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == Config.UserId)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    //get user from db (find user by user id)
                    var user = await _userService.FindAsync(userId);

                    // issue the claims for the user
                    if (user != null)
                    {
                        var claims = ResourceOwnerPasswordValidator.GetUserClaims(user);

                        context.IssuedClaims = claims.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userId = context.Subject.Claims.FirstOrDefault(x => x.Type == Config.UserId);

                if (!string.IsNullOrEmpty(userId?.Value))
                {
                    var user = await _userService.FindAsync(userId.Value);

                    context.IsActive = user != null;
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}
