using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.Managers.Implementations;
using CodeSectorCMS.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CodeSectorCMS.Web.Controllers
{
    public class APIKeyController : BaseController
    {
        private readonly IAPIKeyManager apiKManager;

        public APIKeyController(ILogger<APIKeyController> logger, 
            IAPIKeyManager aPIKeyManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.apiKManager = aPIKeyManager;
        }

        //
        // GET: /apiktemp/

        public IActionResult Index()
        {
            return View(apiKManager.GetAllAPIKeys(UserId));
        }

        //
        // GET: /apiktemp/Details/5

        public IActionResult Details(int id = 0)
        {
            APIKey apikey = apiKManager.GetAPIKeyByID(UserId, id);
            
            return View(apikey);
        }

        //
        // GET: /apiktemp/Create

        public IActionResult Create()
        {
            ViewBag.UserId = UserId;
            return View();
        }

        //
        // POST: /apiktemp/Create

        [HttpPost]
        public IActionResult Create(APIKey apikey)
        {
            if (ModelState.IsValid)
            {
                apiKManager.SaveAPIKey(apikey);
                apiKManager.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(apikey);
        }

        //
        // GET: /apiktemp/Edit/5

        public IActionResult Edit(int id = 0)
        {
            APIKey apikey = apiKManager.GetAPIKeyByID(UserId, id);
            
            return View(apikey);
        }

        //
        // POST: /apiktemp/Edit/5

        [HttpPost]
        public IActionResult Edit(APIKey apikey)
        {
            if (ModelState.IsValid)
            {
                apiKManager.Update(apikey);
                apiKManager.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apikey);
        }

        //
        // GET: /apiktemp/Delete/5

        public IActionResult Delete(int id = 0)
        {
            APIKey apikey = apiKManager.GetAPIKeyByID(UserId, id);
            
            return View(apikey);
        }

        //
        // POST: /apiktemp/Delete/5

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            APIKey apikey = apiKManager.GetAPIKeyByID(UserId, id);
            apiKManager.RemoveAPIKeyByID(UserId, id);
            apiKManager.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
