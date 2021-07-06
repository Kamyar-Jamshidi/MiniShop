using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniShop.Api.Model.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MiniShop.Api.Model.Authentication
{
    public class AuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly AuthenticateResult failResult;

        public AuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticationManager authenticationManager)
            : base(options, logger, encoder, clock)
        {
            this.authenticationManager = authenticationManager;
            failResult = AuthenticateResult.Fail("Unauthorized");
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return failResult;

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader) ||
                !authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
                return failResult;

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
                return failResult;

            try
            {
                return ValidateToken(token);
            }
            catch (Exception)
            {
                return failResult;
            }
        }

        private AuthenticateResult ValidateToken(string token)
        {
            var validatedToken = authenticationManager.Tokens.FirstOrDefault(t => t.Token == token);
            if (validatedToken == null)
                return failResult;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, validatedToken.Key),
            };

            claims.AddRange(validatedToken.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new GenericPrincipal(identity, validatedToken.Roles.ToArray());
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
