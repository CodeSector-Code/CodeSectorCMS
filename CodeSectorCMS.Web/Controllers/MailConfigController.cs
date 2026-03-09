using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class MailConfigController : BaseController
    {
        private readonly IMailConfigManager mailConfigManager;

        public MailConfigController(ILogger<APIKeyController> logger,
            IMailConfigManager mailConfigManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, userManager, signInManager)
        {
            this.mailConfigManager = mailConfigManager;
        }

        public ActionResult Index()
        {
            return View(mailConfigManager.GetAllMailConfigs(ClientId));
        }

        public ActionResult Details(int id = 0)
        {
            MailConfig mailconfig = mailConfigManager.GetAllMailConfigs(ClientId).Where(m => m.MailConfigID == id).First();
            return View(mailconfig);
        }

        public ActionResult Create()
        {
            ViewBag.ClientId = ClientId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(MailConfig mailconfig)
        {
            if (ModelState.IsValid)
            {
                mailConfigManager.CreateNewMailConfig(mailconfig);
                mailConfigManager.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mailconfig);
        }

        public ActionResult Edit(int id = 0)
        {
            MailConfig mailconfig = mailConfigManager.GetMailConfigByID(id);
            return View(mailconfig);
        }

        [HttpPost]
        public ActionResult Edit(MailConfig mailconfig)
        {
            if (ModelState.IsValid)
            {
                mailConfigManager.SaveMailConfig(mailconfig);
                mailConfigManager.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mailconfig);
        }

        public ActionResult Delete(int id = 0)
        {
            MailConfig mailconfig = mailConfigManager.GetAllMailConfigs(ClientId).Where(m => m.MailConfigID == id).First();
            return View(mailconfig);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            mailConfigManager.RemoveMailConfigByID(id);
            mailConfigManager.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            mailConfigManager.Dispose();
        }
    }
}
