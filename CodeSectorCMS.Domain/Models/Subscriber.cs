using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class Subscriber : BaseEntity
    {
        public int SubscriberID { get; set; }
        public int ClientID { get; set; }

        //[Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "E-mail")]
        //[MaxLength(50)]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Client Client { get; set; }
        public virtual ICollection<SubscriberCustomFieldValue> SubscriberCustomFieldValues { get; set; }
        public virtual ICollection<SubscriberGroup> SubscriberGroups { get; set; }
    }
}
