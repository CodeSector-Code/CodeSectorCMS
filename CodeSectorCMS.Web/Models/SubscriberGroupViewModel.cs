using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Web.Models
{
    public class SubscriberGroupViewModel
    {
        public SubscriberGroupViewModel()
        {
            Subscribers = new List<SubscriberGroupModels>();
        }
        [Required(ErrorMessage = "Name must be set")]
        
        [Display(Name = "Subscriber Group Name")]
        public string Name { get; set; }
        public int GroupId { get; set; }
        public List<SubscriberGroupModels> Subscribers { get; set; }
        public List<SubscriberGroupModel2>? SubscriberGroups { get; set; }
        
    }
}
