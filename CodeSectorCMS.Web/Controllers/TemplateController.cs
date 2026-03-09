using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using CodeSectorCMS.Web.Models;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class TemplateController : BaseController
    {
        private readonly ITemplateManager templateMenager;
        private readonly ICustomFieldsManager customFieldManager;
        public TemplateController(ILogger<APIKeyController> logger,
            ITemplateManager templateMenager,
            ICustomFieldsManager customFieldManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, userManager, signInManager)
        {
            this.templateMenager = templateMenager;
            this.customFieldManager = customFieldManager;
        }

        public ActionResult Index()
        {
            return View(templateMenager.GetAllTemplate(ClientId));
        }

        public ActionResult Details(int id = 0)
        {
            Template template = templateMenager.GetTemplateByID(ClientId, id).First();
            return View(template);
        }

        public ActionResult Create()
        {
            var template = new TemplateViewModel
            {
                customFields = customFieldManager.GetAllCustomFields(ClientId).ToList()
            };
            return View(template);
        }

        [HttpPost]
        public ActionResult Create(IFormCollection template)
        {
            if (ModelState.IsValid)
            {

                Template t = new Template
                {
                    ClientID = ClientId,
                    Name = template["Name"],
                    Subject = template["Subject"],
                    Body = template["Body"]
                };
                templateMenager.CreateNewTemplate(t);
                templateMenager.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(template);
        }

        public ActionResult Edit(int id = 0)
        {
            TemplateViewModel template = templateMenager.GetTemplateByID(ClientId, id)
                .Select(t => new TemplateViewModel { TemplateID = t.TemplateID, ClientID = t.ClientID, Name = t.Name, Subject = t.Subject, Body = t.Body, Campaigns = t.Campaigns }).First();
            template.customFields = customFieldManager.GetAllCustomFields(ClientId).ToList();

            return View(template);
        }

        [HttpPost]
        public ActionResult Edit(IFormCollection template)
        {
            if (ModelState.IsValid)
            {
                Template t = new Template
                {
                    TemplateID = Int32.Parse(template["TemplateID"]),
                    ClientID = ClientId,
                    Name = template["Name"],
                    Subject = template["Subject"],
                    Body = template["Body"]
                };
                templateMenager.UpdateTemplate(t);
                templateMenager.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(template);
        }

        public ActionResult Delete(int id = 0)
        {
            Template template = templateMenager.GetTemplateByID(ClientId, id).First();
            return View(template);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Template template = templateMenager.GetTemplateByID(ClientId, id).First();

            templateMenager.DeleteTemplateByID(ClientId, id);
            templateMenager.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            templateMenager.Dispose();
        }
    }
}
