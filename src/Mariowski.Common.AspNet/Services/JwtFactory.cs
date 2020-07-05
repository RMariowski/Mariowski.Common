using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Mariowski.Common.AspNet.Services
{
    public class JwtFactory : IJwtFactory
    {
        /// <inheritdoc />
        public string CreateToken(string signingKey, string issuer, string audience,
            IEnumerable<Claim> claims, DateTime? notBefore, DateTime? expires)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));

            var token = new JwtSecurityToken(issuer, audience, claims, notBefore, expires,
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}