using JuCheap.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace JuCheap.Core.Web.Controllers
{
    public class BaseController : Controller
    {
        public CurrentUserDto GetCurrentUser()
        {
            return new CurrentUserDto
            {
                UserId = User.Identity.GetLoginUserId(),
                LoginName = User.Identity.Name
            };
        }
    }
}