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
        private readonly IClientManager clientManager;
        private readonly IAccountManager accountManager;

        public CampaignController(ILogger<APIKeyController> logger,
            ICampaignManager campaignManager,
            IClientManager clientManager,
            IAccountManager accountManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(logger, userManager, signInManager)
        {
            this.campaignManager = campaignManager;
            this.clientManager = clientManager;
            this.accountManager = accountManager;
        }

        //
        // GET: /Campaign/

        public ActionResult Index()
        {
            var campaigns = campaignManager.GetAllCampaigns().Where(c => c.ClientID == ClientId);
            return View(campaigns.ToList());
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
            Client client = clientManager.GetClientForTemplateByID(ClientId);
            ViewBag.SubscriberGroupID = new SelectList(client.SubscriberGroups, "SubscriberGroupID", "Name");
            ViewBag.MailConfigID = new SelectList(client.MailConfigs, "MailConfigID", "Email");
            ViewBag.TemplateID = new SelectList(client.Templates, "TemplateID", "Name");
            return View();
        }

        //
        // POST: /Campaign/Create

        [HttpPost]
        public ActionResult Create(Campaign campaign)
        {
            //if (ModelState.IsValid)
            //{
                Account acc = accountManager.GetAllAccounts(ClientId).First();

                var request = new Request
                {
                    ClientID = ClientId,
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

            Client client = clientManager.GetClientForTemplateByID(ClientId);
            ViewBag.SubscriberGroupID = new SelectList(client.SubscriberGroups, "SubscriberGroupID", "Name", campaign.SubscriberGroupID);
            ViewBag.MailConfigID = new SelectList(client.MailConfigs, "MailConfigID", "Email", campaign.MailConfigID);
            ViewBag.TemplateID = new SelectList(client.Templates, "TemplateID", "Name", campaign.TemplateID);
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
            var campaigns = campaignManager.GetAllCampaigns().Where(c => c.ClientID == ClientId);
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
            clientManager.Dispose();
            campaignManager.Dispose();
            base.Dispose(disposing);
        }
    }
}
