using BookLibraryManagerBL.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookLibraryManagerBL.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthOptions _authOptions;

        public TokenGenerator(IOptions<AuthOptions> options)
        {
            _authOptions = options.Value;
        }

        public string GenerateToken(string userName, string role)
        {
            var identity = GetIdentity(userName, role);

            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(_authOptions.LifetimeInSeconds)),
                    signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key)),
                    SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(string username, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
