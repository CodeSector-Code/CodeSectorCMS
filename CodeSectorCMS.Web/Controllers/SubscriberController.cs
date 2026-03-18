using System;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class SubscriberController : BaseController
    {
        private readonly ISubscriberManager subscriberManager;
        private readonly ICustomFieldsManager customFieldsManager;
        private readonly ISubscriberCustomFieldValueManager subscriberCustomFieldValueManager;

        public SubscriberController(ILogger<APIKeyController> logger,
            ISubscriberManager subscriberManager,
            ICustomFieldsManager customFieldsManager,
            ISubscriberCustomFieldValueManager subscriberCustomFieldValueManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.subscriberManager = subscriberManager;
            this.customFieldsManager = customFieldsManager;
            this.subscriberCustomFieldValueManager = subscriberCustomFieldValueManager;
        }

        //
        // GET: /Subscriber/

        public ActionResult Index()
        {
            var subscribers = subscriberManager.GetAllSubscribers(UserId).Select(s => new ExtendedSubscriber
            {
                UserId = s.UserId,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                User = s.User
            });

            return View(subscribers.ToList());
        }

        //
        // GET: /Subscriber/Details/5

        public ActionResult Details(int id = 0)
        {
            ExtendedSubscriber subscriber = subscriberManager.GetAllSubscribersWithCustomFieldValues(UserId).Where(s => s.SubscriberID == id).Select(s => new ExtendedSubscriber
            {
                UserId = s.UserId,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                User = s.User,
                CustomFields = customFieldsManager.GetAllCustomFields(UserId).ToList(),
                SubscriberCustomFieldValues = subscriberCustomFieldValueManager.GetAllSubscriberCustomFieldValue(s.SubscriberID).ToList()
            }).First();

            List<SubscriberCustomFieldValue> list = subscriber.SubscriberCustomFieldValues.ToList();

            return View(subscriber);
        }

        //
        // GET: /Subscriber/Create

        public ActionResult Create()
        {
            ViewBag.UserId = UserId;
            ExtendedSubscriber extendedSubscriber = new ExtendedSubscriber
            {
                CustomFields = customFieldsManager.GetAllCustomFields(UserId).ToList()
            };

            return View(extendedSubscriber);
        }

        //
        // POST: /Subscriber/Create

        [HttpPost]
        public ActionResult Create(IFormCollection subscriber)
        {
            if (ModelState.IsValid)
            {
                Subscriber s = new Subscriber
                {
                    UserId = UserId,
                    Name = subscriber["Name"],
                    Email = subscriber["Email"],
                    PhoneNumber = subscriber["PhoneNumber"]
                };
                subscriberManager.CreateNewSubscriber(s);
                subscriberManager.SaveChanges();

                int subscriberID = s.SubscriberID;

                ExtendedSubscriber exS = new ExtendedSubscriber
                {
                    CustomFields = customFieldsManager.GetAllCustomFields(UserId).ToList()
                };

                foreach (var item in exS.CustomFields)
                {
                    int customFieldID = item.CustomFieldID;
                    string customFieldName = item.Name;

                    SubscriberCustomFieldValue scfv = new SubscriberCustomFieldValue
                    {
                        CustomFieldID = customFieldID,
                        SubscriberID = subscriberID,
                        Value = subscriber[customFieldName]
                    };

                    subscriberCustomFieldValueManager.CreateNewSubscriberCustomFieldValue(scfv);
                    subscriberCustomFieldValueManager.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(subscriber);
        }

        //
        // GET: /Subscriber/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ExtendedSubscriber subscriber = subscriberManager.GetAllSubscribersWithCustomFieldValues(UserId).Where(s => s.SubscriberID == id).Select(s => new ExtendedSubscriber
            {
                UserId = s.UserId,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                User = s.User,
                CustomFields = customFieldsManager.GetAllCustomFields(UserId).ToList(),
                SubscriberCustomFieldValues = subscriberCustomFieldValueManager.GetAllSubscriberCustomFieldValue(s.SubscriberID).ToList()
            }).First();

            return View(subscriber);
        }

        //
        // POST: /Subscriber/Edit/5

        [HttpPost]
        public ActionResult Edit(IFormCollection subscriber)
        {
            if (ModelState.IsValid)
            {

                Subscriber s = new Subscriber
                {
                    SubscriberID = Int32.Parse(subscriber["SubscriberID"]),
                    UserId = UserId,
                    Name = subscriber["Name"],
                    Email = subscriber["Email"],
                    PhoneNumber = subscriber["PhoneNumber"]
                };
                subscriberManager.UpdateSubscriber(s);
                subscriberManager.SaveChanges();

                int subscriberID = s.SubscriberID;

                ExtendedSubscriber exS = new ExtendedSubscriber
                {
                    CustomFields = customFieldsManager.GetAllCustomFields(UserId).ToList()
                };

                foreach (var item in exS.CustomFields)
                {
                    int customFieldID = item.CustomFieldID;
                    string customFieldName = item.Name;

                    SubscriberCustomFieldValue scfv = new SubscriberCustomFieldValue
                    {
                        SubscriberCustomFieldValueID = Int32.Parse(subscriber["SubscriberCustomFieldValue" + item.CustomFieldID]),
                        CustomFieldID = customFieldID,
                        SubscriberID = subscriberID,
                        Value = subscriber[customFieldName]
                    };

                    subscriberCustomFieldValueManager.UpdateSubscriberCustomFieldValue(scfv);
                    subscriberCustomFieldValueManager.SaveChanges();


                }
                return RedirectToAction("Index");
            }
            return View(subscriber);
        }

        //
        // GET: /Subscriber/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ExtendedSubscriber subscriber = subscriberManager.GetAllSubscribersWithCustomFieldValues(UserId).Where(s => s.SubscriberID == id).Select(s => new ExtendedSubscriber
            {
                UserId = s.UserId,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                User = s.User,
                CustomFields = customFieldsManager.GetAllCustomFields(UserId).ToList(),
                SubscriberCustomFieldValues = subscriberCustomFieldValueManager.GetAllSubscriberCustomFieldValue(s.SubscriberID).ToList()
            }).First();

            return View(subscriber);
        }

        //
        // POST: /Subscriber/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            subscriberManager.DeleteSubscriberByID(UserId, id);
            subscriberManager.SaveChanges();

            return RedirectToAction("Index");
        }

        /*

        public ActionResult ReadFile()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult ReadFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string temp = fileName;
                temp = temp.Substring(temp.IndexOf('.'));
                if (!temp.Equals(".csv"))
                    return RedirectToAction("Index");

                var path = Path.Combine(Server.MapPath("~/CSV"), fileName);
                file.SaveAs(path);

                contentFromFile(path);

                System.IO.File.Delete(path);
            }
            return RedirectToAction("Index");
        }
        private void contentFromFile(string path)
        {
            CustomField[] customFields = customFieldsManager.GetAllCustomFields(UserId).ToArray();
            string[][] properties = System.IO.File.ReadLines(path).Where(c => c != "").Select(c => c.Split(new[] { ',' })).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                string[] temp = properties[i];
                try
                {
                    Subscriber subscriber = new Subscriber
                    {
                        UserId = UserId,
                        Name = temp[0],
                        Email = temp[1],
                        PhoneNumber = temp[2]
                    };

                    try
                    {
                        subscriberManager.CreateNewSubscriber(subscriber);
                        subscriberManager.SaveChanges();

                        //foreach (var customField in customFields)
                        //{
                        //    SubscriberCustomFieldValue scfv = new SubscriberCustomFieldValue
                        //    {
                        //        CustomFieldID = customField.CustomFieldID,
                        //        SubscriberID = subscriber.SubscriberID,
                        //        Value = ""
                        //    };
                        //    subscriberCustomFieldValueManager.CreateNewSubscriberCustomFieldValue(scfv);
                        //    subscriberCustomFieldValueManager.SaveChanges();
                        //}

                        for (int j = 3; j < temp.Length; j++)
                        {
                            SubscriberCustomFieldValue scfv = new SubscriberCustomFieldValue
                            {
                                CustomFieldID = customFields[j - 3].CustomFieldID,
                                SubscriberID = subscriber.SubscriberID,
                                Value = temp[j]
                            };
                            try
                            {
                                subscriberCustomFieldValueManager.CreateNewSubscriberCustomFieldValue(scfv);
                                subscriberCustomFieldValueManager.SaveChanges();
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
                catch { }
            }
        }
        
         */

        protected override void Dispose(bool disposing)
        {
            subscriberManager.Dispose();
        }
    }
}
