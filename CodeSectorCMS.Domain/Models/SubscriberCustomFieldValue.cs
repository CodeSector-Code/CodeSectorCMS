using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class SubscriberCustomFieldValue : BaseEntity
    {
       public int SubscriberCustomFieldValueID { get; set; }
       public int CustomFieldID { get; set; }
       public int SubscriberID { get; set; }

       public string Value { get; set; }

       public virtual Subscriber Subscriber { get; set; }
       public virtual CustomField CustomField { get; set; }
    }
}
