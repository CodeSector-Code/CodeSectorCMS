using System;

namespace CodeSectorCMS.Domain.MessageModels
{
    public class Request
    {
        public Guid APIkey { get; set; }
        public int? SubsrciberGroupID { get; set; }
        public int? TemplateID { get; set; }
        public int? UserId { get; set; }
        public int? AccountID { get; set; }
        public string CampaignDescription { get; set; }
        public string CampaignName { get; set; }
        public int MailConfigId { get; set; }

    }
}
