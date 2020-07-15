using System;
using System.Collections.Generic;
using System.Security.Claims;
using Mariowski.Common.Markers;

namespace Mariowski.Common.AspNet.Services
{
    public interface IJwtFactory : IService
    {
        /// <summary>
        /// Creates JSON Web Token.
        /// </summary>
        /// <param name="signingKey">Key to create a <see cref="T:SymmetricSecurityKey"/> for signing JWT.</param>
        /// <param name="issuer">If this value is not null, a { iss, 'issuer' } claim will be added, overwriting any 'iss' claim in 'claims' if present.</param>
        /// <param name="audience">If this value is not null, a { aud, 'audience' } claim will be added, appending to any 'aud' claims in 'claims' if present.</param>
        /// <param name="claims">If this value is not null then for each <see cref="Claim"/> a { 'Claim.Type', 'Claim.Value' } is added. If duplicate claims are found then a { 'Claim.Type', List&lt;object&gt; } will be created to contain the duplicate values.</param>
        /// <param name="notBefore">If notBefore.HasValue a { nbf, 'value' } claim is added, overwriting any 'nbf' claim in 'claims' if present.</param>
        /// <param name="expires">If expires.HasValue a { exp, 'value' } claim is added, overwriting any 'exp' claim in 'claims' if present.</param>
        /// <returns>Created token</returns>
        string CreateToken(string signingKey, string issuer, string audience, IEnumerable<Claim> claims,
            DateTime? notBefore, DateTime? expires);
    }
}