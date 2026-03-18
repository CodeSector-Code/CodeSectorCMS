using System;
using Microsoft.AspNetCore.Identity;

namespace CodeSectorCMS.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public int UserId { get; set; }
    }
}
