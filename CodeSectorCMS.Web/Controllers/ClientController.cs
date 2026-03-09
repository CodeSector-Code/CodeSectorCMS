using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IClientManager clientManager;

        public ClientController(ILogger<APIKeyController> logger,
            IClientManager clientManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, userManager, signInManager)
        {
            this.clientManager = clientManager;
        }

        //
        // GET: /Client/

        public IActionResult Index()
        {
            return View(clientManager.GetAllClients());
        }

        //
        // GET: /Client/Details/5

        public IActionResult Details(int id = 0)
        {
            Client client = clientManager.GetClientByID(id);
            return View(client);
        }

        //
        // GET: /Client/Create

        public IActionResult Create()
        {
            return View();
        }

        //
        // POST: /Client/Create

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                clientManager.CreateNewClient(client);
                clientManager.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(client);
        }

        //
        // GET: /Client/Edit/5

        public IActionResult Edit(int id = 0)
        {
            Client client = clientManager.GetClientByID(id);
            return View(client);
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                clientManager.SaveClient(client);
                clientManager.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(client);
        }

        //
        // GET: /Client/Delete/5

        public IActionResult Delete(int id = 0)
        {

            Client client = clientManager.GetClientByID(id);
            return View(client);
        }

        //
        // POST: /Client/Delete/5

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            clientManager.RemoveClientByID(id);
            clientManager.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
