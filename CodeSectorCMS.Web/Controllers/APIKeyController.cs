using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class APIKeyController : BaseController
    {
        private readonly IAPIKeyManager apiKManager;

        public APIKeyController(ILogger<APIKeyController> logger, 
            IAPIKeyManager aPIKeyManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, userManager, signInManager)
        {
            this.apiKManager = aPIKeyManager;
        }

        //
        // GET: /apiktemp/

        public IActionResult Index()
        {
            return View(apiKManager.GetAllAPIKeys(ClientId));
        }

        //
        // GET: /apiktemp/Details/5

        public IActionResult Details(int id = 0)
        {
            APIKey apikey = apiKManager.GetAPIKeyByID(ClientId, id);
            
            return View(apikey);
        }

        //
        // GET: /apiktemp/Create

        public IActionResult Create()
        {
            ViewBag.ClientId = ClientId;
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
            APIKey apikey = apiKManager.GetAPIKeyByID(ClientId, id);
            
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
            APIKey apikey = apiKManager.GetAPIKeyByID(ClientId, id);
            
            return View(apikey);
        }

        //
        // POST: /apiktemp/Delete/5

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            APIKey apikey = apiKManager.GetAPIKeyByID(ClientId, id);
            apiKManager.RemoveAPIKeyByID(ClientId, id);
            apiKManager.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
