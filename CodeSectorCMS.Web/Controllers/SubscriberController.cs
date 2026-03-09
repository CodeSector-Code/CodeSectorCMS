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
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, userManager, signInManager)
        {
            this.subscriberManager = subscriberManager;
            this.customFieldsManager = customFieldsManager;
            this.subscriberCustomFieldValueManager = subscriberCustomFieldValueManager;
        }

        //
        // GET: /Subscriber/

        public ActionResult Index()
        {
            var subscribers = subscriberManager.GetAllSubscribers(ClientId).Select(s => new ExtendedSubscriber
            {
                ClientID = s.ClientID,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Client = s.Client
            });

            return View(subscribers.ToList());
        }

        //
        // GET: /Subscriber/Details/5

        public ActionResult Details(int id = 0)
        {
            ExtendedSubscriber subscriber = subscriberManager.GetAllSubscribersWithCustomFieldValues(ClientId).Where(s => s.SubscriberID == id).Select(s => new ExtendedSubscriber
            {
                ClientID = s.ClientID,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Client = s.Client,
                CustomFields = customFieldsManager.GetAllCustomFields(ClientId).ToList(),
                SubscriberCustomFieldValues = subscriberCustomFieldValueManager.GetAllSubscriberCustomFieldValue(s.SubscriberID).ToList()
            }).First();

            List<SubscriberCustomFieldValue> list = subscriber.SubscriberCustomFieldValues.ToList();

            return View(subscriber);
        }

        //
        // GET: /Subscriber/Create

        public ActionResult Create()
        {
            ViewBag.ClientId = ClientId;
            ExtendedSubscriber extendedSubscriber = new ExtendedSubscriber
            {
                CustomFields = customFieldsManager.GetAllCustomFields(ClientId).ToList()
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
                    ClientID = ClientId,
                    Name = subscriber["Name"],
                    Email = subscriber["Email"],
                    PhoneNumber = subscriber["PhoneNumber"]
                };
                subscriberManager.CreateNewSubscriber(s);
                subscriberManager.SaveChanges();

                int subscriberID = s.SubscriberID;

                ExtendedSubscriber exS = new ExtendedSubscriber
                {
                    CustomFields = customFieldsManager.GetAllCustomFields(ClientId).ToList()
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
            ExtendedSubscriber subscriber = subscriberManager.GetAllSubscribersWithCustomFieldValues(ClientId).Where(s => s.SubscriberID == id).Select(s => new ExtendedSubscriber
            {
                ClientID = s.ClientID,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Client = s.Client,
                CustomFields = customFieldsManager.GetAllCustomFields(ClientId).ToList(),
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
                    ClientID = ClientId,
                    Name = subscriber["Name"],
                    Email = subscriber["Email"],
                    PhoneNumber = subscriber["PhoneNumber"]
                };
                subscriberManager.UpdateSubscriber(s);
                subscriberManager.SaveChanges();

                int subscriberID = s.SubscriberID;

                ExtendedSubscriber exS = new ExtendedSubscriber
                {
                    CustomFields = customFieldsManager.GetAllCustomFields(ClientId).ToList()
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
            ExtendedSubscriber subscriber = subscriberManager.GetAllSubscribersWithCustomFieldValues(ClientId).Where(s => s.SubscriberID == id).Select(s => new ExtendedSubscriber
            {
                ClientID = s.ClientID,
                SubscriberID = s.SubscriberID,
                Name = s.Name,
                Email = s.Email,
                PhoneNumber = s.PhoneNumber,
                Client = s.Client,
                CustomFields = customFieldsManager.GetAllCustomFields(ClientId).ToList(),
                SubscriberCustomFieldValues = subscriberCustomFieldValueManager.GetAllSubscriberCustomFieldValue(s.SubscriberID).ToList()
            }).First();

            return View(subscriber);
        }

        //
        // POST: /Subscriber/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            subscriberManager.DeleteSubscriberByID(ClientId, id);
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
            CustomField[] customFields = customFieldsManager.GetAllCustomFields(ClientId).ToArray();
            string[][] properties = System.IO.File.ReadLines(path).Where(c => c != "").Select(c => c.Split(new[] { ',' })).ToArray();

            for (int i = 0; i < properties.Length; i++)
            {
                string[] temp = properties[i];
                try
                {
                    Subscriber subscriber = new Subscriber
                    {
                        ClientID = ClientId,
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
