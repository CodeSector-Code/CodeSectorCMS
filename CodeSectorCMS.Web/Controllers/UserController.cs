using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserManager userManager;

        public UserController(ILogger<APIKeyController> logger,
            IUserManager userManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.userManager = userManager;
        }

        //
        // GET: /User/

        public IActionResult Index()
        {
            return View(userManager.GetAllUsers());
        }

        //
        // GET: /User/Details/5

        public IActionResult Details(int id = 0)
        {
            User user = userManager.GetUserByID(id);
            return View(user);
        }

        //
        // GET: /User/Create

        public IActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userManager.CreateNewUser(user);
                userManager.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        //
        // GET: /User/Edit/5

        public IActionResult Edit(int id = 0)
        {
            User user = userManager.GetUserByID(id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userManager.SaveUser(user);
                userManager.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5

        public IActionResult Delete(int id = 0)
        {

            User user = userManager.GetUserByID(id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            userManager.RemoveUserByID(id);
            userManager.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
