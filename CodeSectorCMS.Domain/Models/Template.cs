using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class Template : BaseEntity
    {
       public int TemplateID { get; set; }
       public int UserId { get; set; }

       //[Required(ErrorMessage = "Name is required.")]
       public string Name { get; set; }

       //[Required(ErrorMessage = "Subject is required.")]
       public string Subject { get; set; }

       //[Required(ErrorMessage = "Body is required.")]
       public string Body { get; set; }

       public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
