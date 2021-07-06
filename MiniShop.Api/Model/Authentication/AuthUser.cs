using System.Collections.Generic;

namespace MiniShop.Api.Model.Authentication
{
    public class AuthUser
    {
        public AuthUser(string token, string key, params string[] roles)
        {
            Token = token;
            Key = key;
            Roles = roles;
        }

        public string Token { get; set; }
        public string Key { get; set; }
        public IList<string> Roles { get; set; }
    }
}