using System;
using System.Collections.Generic;

namespace CodeSectorCMS.Domain
{
    public class SubscriberGroupSubscriberEntity : BaseEntity
    {
        public int SubscriberGroupSubscriberEntityID { get; set; }
        public int SubscriberID { get; set; }
        public int SubscriberGroupID { get; set; }

        public virtual Subscriber subscriber { get; set; }
        public virtual SubscriberGroup subscriberGroup { get; set; }
    }
}
