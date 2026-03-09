using System;

namespace CodeSectorCMS.Domain
{
    public class BaseEntity
    {
        public string DateCreated { get; set; } = DateTime.Now.ToShortDateString();
        public bool Deleted { get; set; }

    }
}
