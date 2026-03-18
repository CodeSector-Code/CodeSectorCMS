using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class SubscriberGroup : BaseEntity
    {
        public int SubscriberGroupID { get; set; }

        //[Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }

        //[Required(ErrorMessage = "Subscriber group can not be empty.")]
        public virtual ICollection<Subscriber> Subscribers { get; set; }
    }
}
