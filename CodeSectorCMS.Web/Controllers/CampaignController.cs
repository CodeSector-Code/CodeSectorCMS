using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

using CodeSectorCMS.Domain;
using CodeSectorCMS.Domain.MessageModels;
using CodeSectorCMS.Domain.Managers.Interfaces;

namespace CodeSectorCMS.Web.Controllers
{
    public class CampaignController : BaseController
    {
        private readonly ICampaignManager campaignManager;
        private readonly IUserManager userManager;
        private readonly IAccountManager accountManager;
        private readonly ITrackMessageManager trackMessageManager;

        public CampaignController(ILogger<APIKeyController> logger,
            ICampaignManager campaignManager,
            IUserManager userManager,
            IAccountManager accountManager,
            ITrackMessageManager trackMessageManager,
            UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, appUserManager, signInManager)
        {
            this.campaignManager = campaignManager;
            this.userManager = userManager;
            this.accountManager = accountManager;
            this.trackMessageManager = trackMessageManager;
        }

        //
        // GET: /Campaign/

        public ActionResult Index()
        {
            var extendedCampaigns = new List<ExtendedCampaign>();

            var campaigns = campaignManager.GetAllCampaignsWithMessages().Where(c => c.UserId == UserId);

            foreach (var item in campaigns)
            {
                var trackMessages = new List<TrackMessage>();

                foreach (var message in item.Messages)
                {
                    trackMessages.Add(trackMessageManager.GetTrackedMessageByMessageId(message.MessageID));
                }

                extendedCampaigns.Add(new ExtendedCampaign { Campaign = item, TrackMessage = trackMessages });
            }
            return View(extendedCampaigns);
        }

        //
        // GET: /Campaign/Details/5

        public ActionResult Details(int id = 0)
        {
            Campaign campaign = campaignManager.GetCampaignByID(id);
            return View(campaign);
        }

        //
        // GET: /Campaign/Create

        public ActionResult Create()
        {
            User user = userManager.GetUserForTemplateByID(UserId);
            ViewBag.SubscriberGroupID = new SelectList(user.SubscriberGroups, "SubscriberGroupID", "Name");
            ViewBag.MailConfigID = new SelectList(user.MailConfigs, "MailConfigID", "Email");
            ViewBag.TemplateID = new SelectList(user.Templates, "TemplateID", "Name");
            return View();
        }

        //
        // POST: /Campaign/Create

        [HttpPost]
        public ActionResult Create(Campaign campaign)
        {
            //if (ModelState.IsValid)
            //{
                Account acc = accountManager.GetAllAccounts(UserId).First();

                var request = new Request
                {
                    UserId = UserId,
                    AccountID = acc.AccountID,
                    TemplateID = campaign.TemplateID,
                    SubsrciberGroupID = campaign.SubscriberGroupID,
                    CampaignName = campaign.Name,
                    CampaignDescription = campaign.Description,
                    MailConfigId = campaign.MailConfigID
                };

                // ServiceProducer prod = new ServiceProducer(req, "requestqueue");
                campaignManager.CreateNewCampaign(request);
                campaignManager.SaveChanges();

                ViewBag.CampaignSuccess = "Your Campaign  is Successfuly sent!";
                return RedirectToAction("Index");
            //}

            User user = userManager.GetUserForTemplateByID(UserId);
            ViewBag.SubscriberGroupID = new SelectList(user.SubscriberGroups, "SubscriberGroupID", "Name", campaign.SubscriberGroupID);
            ViewBag.MailConfigID = new SelectList(user.MailConfigs, "MailConfigID", "Email", campaign.MailConfigID);
            ViewBag.TemplateID = new SelectList(user.Templates, "TemplateID", "Name", campaign.TemplateID);
            return View(campaign);
        }

        //
        // GET: /Campaign/Edit/5

        public ActionResult Edit(int id = 0)
        {
            return View();
        }

        //
        // POST: /Campaign/Edit/5

        [HttpPost]
        public ActionResult Edit(Campaign campaign)
        {
            return View(campaign);
        }

        //
        // GET: /Campaign/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Campaign campaign = campaignManager.GetCampaignByID(id);
            return View(campaign);
        }

        //
        // POST: /Campaign/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            campaignManager.RemoveCampaignByID(id);
            campaignManager.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MessageView(int id = 0)
        {
            // Message msg = db.Messages.Find(id);
            var campaigns = campaignManager.GetAllCampaigns().Where(c => c.UserId == UserId);
            Message msg = null;
            foreach (Campaign campaign in campaigns)
            {
                msg = campaign.Messages.Where(m => m.MessageID == id).FirstOrDefault();
                if (msg != null)
                    break;
            }

            return View(msg);
        }

        protected override void Dispose(bool disposing)
        {
            accountManager.Dispose();
            userManager.Dispose();
            campaignManager.Dispose();
            base.Dispose(disposing);
        }
    }
}
