using System;
using CodeSectorCMS.Domain;
using CodeSectorCMS.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class SubscriberGroupController : BaseController
    {
        private readonly ISubscriberManager subscriberManager;
        private readonly ISubscriberGroupManager subscriberGroupManager;

        public SubscriberGroupController(ILogger<APIKeyController> logger,
            ISubscriberManager subscriberManager,
            ISubscriberGroupManager subscriberGroupManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.subscriberManager = subscriberManager;
            this.subscriberGroupManager = subscriberGroupManager;
        }

        //
        // GET: /SubscriberGroup/

        public ActionResult Index()
        {
            return View(subscriberGroupManager.GetAllSubscriberGroups(UserId));
        }

        //
        // GET: /SubscriberGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            SubscriberGroup subscribergroup = subscriberGroupManager.GetSubscriberGroupWithSubscribersByID(UserId, id);

            return View(subscribergroup);
        }

        //
        // GET: /SubscriberGroup/Create

        public ActionResult Create()
        {
            var sub = new SubscriberGroupViewModel
            {
                Subscribers = subscriberManager.GetAllSubscribers(UserId).Select(x => new SubscriberGroupModels { SubscriberID = x.SubscriberID, Email = x.Email }).ToList(),
                SubscriberGroups = subscriberGroupManager.GetAllSubscriberGroups(UserId).Select(x => new SubscriberGroupModel2 { SubscriberGroupID = x.SubscriberGroupID, Subscribers = x.Subscribers, Name = x.Name }).ToList()
            };

            return View(sub);
        }

        //
        // POST: /SubscriberGroup/Create

        [HttpPost]
        public ActionResult Create(SubscriberGroupViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var subscribers = new List<Subscriber>();

                foreach (var item in model.Subscribers.Where(x => x.IsSelected))
                {
                    subscribers.Add(subscriberManager.GetSubscriberByID(UserId,item.SubscriberID));
                }

                subscriberGroupManager.CreateNewSubscriberGroup(new SubscriberGroup { SubscriberGroupID = model.GroupId , UserId = UserId, Name = model.Name, Subscribers = subscribers });
                subscriberGroupManager.SaveChanges();

                return RedirectToAction("Index");
            //}

            //return View();
        }

        //
        // GET: /SubscriberGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SubscriberGroup subscribergroup = subscriberGroupManager.GetSubscriberGroupWithSubscribersByID(UserId, id);

            var model = new SubscriberGroupViewModel
            {
                GroupId = subscribergroup.SubscriberGroupID,
                Name = subscribergroup.Name
            };

            var allSubscribers = subscriberManager.GetAllSubscribers(UserId);
            foreach (var item in allSubscribers)
            {
                model.Subscribers.Add(new SubscriberGroupModels
                {
                    Email = item.Email,
                    SubscriberID = item.SubscriberID,
                    IsSelected = subscribergroup.Subscribers.Any(x => x.SubscriberID == item.SubscriberID),
                    Name = item.Name
                });
            }
            return View(model);
        }

        //
        // POST: /SubscriberGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(SubscriberGroupViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var subscriberGroup = subscriberGroupManager.GetSubscriberGroupWithSubscribersByID(UserId, model.GroupId);

                var selectedSubscriberIds = model.Subscribers.Where(s => s.IsSelected).Select(s => s.SubscriberID).ToList();

                var newSubscribers = subscriberManager.GetAllSubscribers(UserId).Where(s => selectedSubscriberIds.Contains(s.SubscriberID));

                subscriberGroup.Subscribers.Clear();


                foreach (var newSubscriber in newSubscribers)
                {
                    subscriberGroup.Subscribers.Add(newSubscriber);
                }
                subscriberGroup.Name = model.Name;

                //subscriberGroupManager.SaveSubscriberGroup(subscriberGroup);
                subscriberGroupManager.SaveChanges();
                subscriberManager.SaveChanges();

                //var subscribers = new List<Subscriber>();
                // var group = db.SubscriberGroups.Find(model.GroupId);
                //for(var item in group.Subscribers)
                //{

                //    group.Subscribers.Remove(item);
                //    //subscribers.Add(db.Subscribers.Where(x => x.UserId == UserId && x.SubscriberID == item.SubscriberID).First());
                //    //group.Subscribers.Remove(
                //        //db.sub
                //}
                //foreach (var item in model.Subscribers.Where(x => x.IsSelected))
                //{

                //}
                //var a = db.SubscriberGroups.Find(model.GroupId);


                //db.SubscriberGroups.Remove(db.SubscriberGroups.Find(model.GroupId));
                //a.Subscribers = subscribers;
                ////a.UserId = UserId;
                //db.Entry(a).State = EntityState.Modified;
                //subscribers[0].
                //db.
                //db.Entry(new SubscriberGroup { SubscriberGroupID = model.GroupId, UserId = UserId, Name = model.Name, Subscribers = subscribers }).State = EntityState.Modified;
                //foreach (Subscriber s in a.Subscribers)
                //{
                //    db.MailConfigs.SubscriberGroupSubscriber 
                //}
                //db.SaveChanges();
                return RedirectToAction("Index");
            //}
            //return View(model);
        }

        //
        // GET: /SubscriberGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SubscriberGroup subscribergroup = subscriberGroupManager.GetSubscriberGroupByID(UserId, id);
            return View(subscribergroup);
        }

        //
        // POST: /SubscriberGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubscriberGroup subscribergroup = subscriberGroupManager.GetSubscriberGroupByID(UserId,id);
            subscriberGroupManager.DeleteSubscriberGroupByID(subscribergroup.SubscriberGroupID);
            subscriberGroupManager.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            subscriberManager.Dispose();
            subscriberGroupManager.Dispose();
        }
    }
}
