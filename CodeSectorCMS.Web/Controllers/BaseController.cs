using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CodeSectorCMS.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> logger;
        protected readonly UserManager<ApplicationUser> appUserManager;
        protected readonly SignInManager<ApplicationUser> signInManager;

        protected BaseController(ILogger<BaseController> logger, 
            UserManager<ApplicationUser> appUserManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.appUserManager = appUserManager;
            this.signInManager = signInManager;
        }

        private async Task<int> GetUserId()
        {
            var userId = -1;

            if (signInManager.IsSignedIn(User))
            {
                var user = await appUserManager.GetUserAsync(User);
                userId = user.UserId;
            }

            return userId;
        }

        protected int UserId { get { return GetUserId().Result; } }
    }
}
