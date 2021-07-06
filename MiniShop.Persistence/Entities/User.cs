using Microsoft.AspNetCore.Identity;
using System;

namespace MiniShop.Persistence.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
