using System;
using CodeSectorCMS.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class CustomfieldController : BaseController
    {
        private readonly ICustomFieldsManager customFieldManager;
        private readonly ISubscriberManager subscriberManager;
        private readonly ISubscriberCustomFieldValueManager subscriberCustomFieldValueManager;

        public CustomfieldController(ILogger<APIKeyController> logger,
            ICustomFieldsManager customFieldManager,
            ISubscriberManager subscriberManager,
            ISubscriberCustomFieldValueManager subscriberCustomFieldValueManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.customFieldManager = customFieldManager;
            this.subscriberManager = subscriberManager;
            this.subscriberCustomFieldValueManager = subscriberCustomFieldValueManager;
        }

        public ActionResult Index()
        {
            var customFields = customFieldManager.GetAllCustomFields(UserId);
            return View(customFields.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UserId = UserId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomField customField)
        {
            customFieldManager.CreateNewCustomField(customField);
            customFieldManager.SaveChanges();

            List<Subscriber> subscribers = subscriberManager.GetAllSubscribers(UserId).ToList();

            foreach (var subscriber in subscribers)
            {
                SubscriberCustomFieldValue scfv = new SubscriberCustomFieldValue
                {
                    CustomFieldID = customField.CustomFieldID,
                    SubscriberID = subscriber.SubscriberID,
                    Value = ""
                };
                subscriberCustomFieldValueManager.CreateNewSubscriberCustomFieldValue(scfv);
                subscriberCustomFieldValueManager.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id = 0)
        {
            CustomField customField = customFieldManager.GetCustomFieldByID(UserId, id);
            return View(customField);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            CustomField customField = customFieldManager.GetCustomFieldByID(UserId, id);
            customFieldManager.DeleteCustomFieldByID(UserId, id);
            customFieldManager.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            CustomField customField = customFieldManager.GetCustomFieldByID(UserId, id);
            return View(customField);
        }

        [HttpPost]
        public ActionResult Edit(CustomField customField)
        {
            customFieldManager.UpdateCustomField(customField);
            customFieldManager.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            customFieldManager.Dispose();
        }
    }
}
