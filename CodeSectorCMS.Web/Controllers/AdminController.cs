using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSectorCMS.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CodeSectorCMS.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly ITrackMessageManager trackMessageManager;

        public AdminController(ILogger<AccountController> logger,
            ITrackMessageManager trackMessageManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.trackMessageManager = trackMessageManager;
        }

        public IActionResult Index()
        {
            return View(trackMessageManager.GetAllTrackedMessages());
        }
    }
}
