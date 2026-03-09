using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeSectorCMS.Domain;
using System.ComponentModel.DataAnnotations;



namespace CodeSectorCMS.Web.Models
{
    public class CampaignModels
    {
        [Display(Name = "Campaign Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Template> Templates { get; set; }
        public IEnumerable<SubscriberGroup> SubscriberGroups { get; set; }
    }
    
}