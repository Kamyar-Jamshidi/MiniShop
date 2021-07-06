using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MiniShop.Core.DTO.Response
{
    public class LoginResponse
    {
        public LoginResponse(string firstName, string lastName, bool isSuperAdmin, params string[] roles)
        {
            FirstName = firstName;
            LastName = lastName;
            IsSuperAdmin = isSuperAdmin;

            Roles = roles;
        }

        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSuperAdmin { get; set; }

        public IEnumerable<string> Roles;
    }
}
