using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class CustomField : BaseEntity
    {
        public int CustomFieldID { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //[Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Label is required.")]
        public string Label { get; set; }

        public virtual ICollection<SubscriberCustomFieldValue> SubscriberCustomFieldValues { get; set; }
    }
}
