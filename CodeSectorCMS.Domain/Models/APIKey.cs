using System;
using System.ComponentModel.DataAnnotations;

namespace CodeSectorCMS.Domain
{
    public class APIKey : BaseEntity
    {
        public int ApiKeyID { get; set; }
        public int ClientID { get; set; }
        public string Description { get; set; } = string.Empty;

        //[Display(Name="APIKey Value")]
        public Guid Value { get; set; }

        public virtual Client? Client { get; set; }
    }
}
