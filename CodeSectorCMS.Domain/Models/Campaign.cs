using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class Campaign : BaseEntity
    {
        public int CampaignID { get; set; }

        //[Display(Name="Campaign name")]
        //[Required(ErrorMessage = "Campaign name is required.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Campaign description is required.")]
        public string Description { get; set; }

        public int ClientID { get; set; }
        public int AccountID { get; set; }
        public int TemplateID { get; set; }
        public int SubscriberGroupID { get; set; }
        public int MailConfigID { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual SubscriberGroup subscriberGroup { get; set; }
        public virtual MailConfig mailConfig { get; set; }
        public virtual Template template { get; set; }
        public virtual Client client { get; set; }
        public virtual Account account { get; set; }



    }
}
