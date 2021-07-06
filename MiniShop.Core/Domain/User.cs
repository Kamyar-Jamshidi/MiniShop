using System.Collections.Generic;

namespace MiniShop.Core.Domain
{
    public class User : BaseDomain
    {
        public new string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
