using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using JuCheap.Core.Interfaces;
using JuCheap.Core.Models;
using JuCheap.Core.Models.Enum;

namespace JuCheap.Core.WebApi
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        //repository to get user from userservice
        private readonly IUserService _userService;

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            _userService = userService; //DI
        }

        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var model = new LoginDto
                {
                    LoginName = context.UserName,
                    Password = context.Password
                };
                var loginResult = await _userService.LoginAsync(model);
                //check if password match - remember to hash password if stored as hash in db
                if (loginResult.Result == LoginResult.Success)
                {
                    //set the result
                    context.Result = new GrantValidationResult(
                        subject: loginResult.User.Id,
                        authenticationMethod: "custom",
                        claims: GetUserClaims(loginResult.User));

                    return;
                }
                else if (loginResult.Result == LoginResult.WrongPassword)
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "密码错误");
                    return;
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "账号不存在");
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "无效的账号或密码");
            }
        }

        //build claims array from user data
        public static Claim[] GetUserClaims(UserDto user)
        {
            return new Claim[]
            {
                new Claim(Config.UserId, user.Id),
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.Name, user.LoginName),
                new Claim(JwtClaimTypes.Email, user.Email)
            };
        }
    }
}
