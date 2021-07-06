using System.Collections.Generic;

namespace MiniShop.Api.Model.Authentication.Interfaces
{
    public interface IAuthenticationManager
    {
        string GetToken(string key, params string[] roles);
        IList<AuthUser> Tokens { get; }
    }
}
