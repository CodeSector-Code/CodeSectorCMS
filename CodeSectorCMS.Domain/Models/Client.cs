using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class Client : BaseEntity
    {
        public int ClientID { get; set; }
        //[Display(Name="Client name")]
        //[Required(ErrorMessage = "Client name is required.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Client description is required.")]
        public string Description { get; set; }

        public virtual ICollection<Subscriber> Subscribers { get; set; }
        public virtual ICollection<SubscriberGroup> SubscriberGroups { get; set; }
        public virtual ICollection<MailConfig> MailConfigs { get; set; }
        public virtual ICollection<APIKey> APIKeys { get; set; }
        public virtual ICollection<Account> Acounts { get; set; }
        public virtual ICollection<CustomField> CustomFields { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
