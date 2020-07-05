using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FluentAssertions;
using Mariowski.Common.AspNet.Services;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Mariowski.Common.AspNet.UnitTests.Services
{
    public class JwtFactoryTests
    {
        [Fact]
        public void CreateToken_ShouldCreateValidJwt()
        {
            const string signingKey = "jwt_signing_key_for_test";
            var parameters = new TokenValidationParameters
            {
                ValidIssuer = "issuer",
                ValidAudience = "audience",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
            };
            var claims = new[] { new Claim("test", "yes") };

            string token = new JwtFactory().CreateToken(signingKey, parameters.ValidIssuer, parameters.ValidAudience,
                claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(1));

            token.Should().NotBeNullOrWhiteSpace();
            _ = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out _);
        }
    }
}