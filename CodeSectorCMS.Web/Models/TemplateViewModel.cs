using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeSectorCMS.Domain;

namespace CodeSectorCMS.Web.Models
{
    public class TemplateViewModel : Template
    {
        public ICollection<CustomField> customFields { get; set; }
    }
}