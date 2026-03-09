using System;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IAccountManager accountManager;
        private readonly IClientManager clientManager;

        public AccountController(ILogger<AccountController> logger, 
            IAccountManager accountManager, 
            IClientManager clientManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger,userManager, signInManager)
        {
            this.accountManager = accountManager;
            this.clientManager = clientManager;
        }

        // GET: /Account/

        public async Task<IActionResult> Index()
        {
            return View(accountManager.GetAllAccounts(ClientID: ClientId));
        }


        //
        // GET: /Account/Details/5

        public IActionResult Details(int id = 0)
        {
            var account = accountManager.GetAccountByID(id);

            return View(account);
        }

        //
        // GET: /Account/Create

        public IActionResult Create()
        {
            return View();
        }


        // POST: /Account/Create

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = new ApplicationUser { UserName = account.Username, Email = account.Email, ClientId = ClientId };
                var result = await userManager.CreateAsync(applicationUser, account.Password);
                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            return View(accountManager.GetAccountByID(id));

        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = accountManager.GetAllAccounts(ClientId).Where(a => a.AccountID == id).First();

            if (accountManager.GetAllAccounts(ClientId).Count() == 1)
            {
                Client client = clientManager.GetClientByID(ClientId);

                clientManager.RemoveClientByID(ClientId);
                clientManager.SaveChanges();
                await Logout();
                return RedirectToAction("Index");
            }

            if (account.AccountID == accountManager.GetAllAccounts(ClientId).Where(a => a.Username == User.Identity.Name).First().AccountID)
            {
                await Logout();
            }

            accountManager.DeleteAccountByID(id);
            accountManager.SaveChanges();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id = 0)
        {
            Account account = accountManager.GetAccountByID(id);
            account.ClientID = ClientId;

            
            return View(account);
        }

        //
        // POST: /MailConfig/Edit/5

        [HttpPost]
        public async Task<IActionResult> Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                account.ClientID = ClientId;

                accountManager.SaveAccount(account);
                accountManager.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(account);
        }

        #region ApplicationUser


        //
        // GET: /Account/Register

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var c = new Client
                {
                    Name = model.Name,
                    Description = model.Description
                };

                clientManager.CreateNewClient(c);
                clientManager.SaveChanges();

                // Attempt to register the user
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, ClientId = c.ClientID };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Save account
                    var acc = new Account
                    {
                        Username = model.UserName,
                        Password = "",
                        Email = model.Email,
                        ClientID = c.ClientID
                    };

                    accountManager.CreateNewAccount(acc);
                    accountManager.SaveChanges();

                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        //
        // GET: /Account/Login

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }
            
            return View(model);
        }

        //
        // POST: /Account/Logout

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        #endregion

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }
        #endregion
    }
}
