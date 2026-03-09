using System;
using Microsoft.AspNetCore.Identity;

namespace CodeSectorCMS.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public int ClientId { get; set; }
    }
}
