using MiniShop.Api.Model.Authentication.Interfaces;
using System;
using System.Collections.Generic;

namespace MiniShop.Api.Model.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IList<AuthUser> tokens = new List<AuthUser>();

        public IList<AuthUser> Tokens => tokens;

        public string GetToken(string key, params string[] roles)
        {
            var token = Guid.NewGuid().ToString();
            tokens.Add(new AuthUser(token, key, roles));
            return token;
        }
    }
}
