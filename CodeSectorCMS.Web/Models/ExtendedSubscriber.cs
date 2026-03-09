using System;
using CodeSectorCMS.Domain;

namespace CodeSectorCMS.Web.Models
{
    public class ExtendedSubscriber : Subscriber
    {
        public ICollection<CustomField> CustomFields { get; set; }
    }
}