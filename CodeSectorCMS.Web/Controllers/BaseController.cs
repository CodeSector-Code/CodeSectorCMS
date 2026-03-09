using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CodeSectorCMS.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> logger;
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;

        protected BaseController(ILogger<BaseController> logger, 
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        private async Task<int> GetClientId()
        {
            var clientId = -1;

            if (signInManager.IsSignedIn(User))
            {
                var user = await userManager.GetUserAsync(User);
                clientId = user.ClientId;
            }

            return clientId;
        }

        protected int ClientId { get { return GetClientId().Result; } }
    }
}
