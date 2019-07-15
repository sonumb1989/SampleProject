using AP.Web.Common.Constants;
using AP.Web.Common.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace AP.Web.Common.Authentication
{
    public class JwtFactory : IJwtFactory
  {
    /// <summary>
    /// Jwt Options 
    /// </summary>
    private readonly JwtIssuerOptions _jwtOptions;

    /// <summary>
    /// JwtFactory 
    /// </summary>
    /// <param name="jwtOptions">The Jwt Options</param>
    public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
    {
      _jwtOptions = jwtOptions.Value;
      ThrowIfInvalidOptions(_jwtOptions);
    }

    /// <summary>
    /// GenerateEncodedToken
    /// </summary>
    /// <param name="userName">userName</param>
    /// <param name="identity">identity</param>
    /// <returns></returns>
    public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
    {
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, userName),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        identity.FindFirst(KeyConstants.Strings.JwtClaimIdentifiers.Rol),
        identity.FindFirst(KeyConstants.Strings.JwtClaimIdentifiers.Id)
      };

      // Create the JWT security token and encode it.
      return new JwtSecurityTokenHandler().WriteToken(
        new JwtSecurityToken(
          issuer: _jwtOptions.Issuer,
          audience: _jwtOptions.Audience,
          claims: claims,
          notBefore: _jwtOptions.NotBefore,
          expires: _jwtOptions.Expiration,
          signingCredentials: _jwtOptions.SigningCredentials));
    }

    /// <summary>
    ///   GenerateClaimsIdentity
    /// </summary>
    /// <param name="userName">userName</param>
    /// <param name="id">id</param>
    /// <returns></returns>
    public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
    {
      return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
      {
        new Claim(KeyConstants.Strings.JwtClaimIdentifiers.Id, id),
        new Claim(KeyConstants.Strings.JwtClaimIdentifiers.Rol, KeyConstants.Strings.JwtClaims.ApiAccess)
      });
    }

    /// <summary>
    /// ToUnixEpochDate
    /// </summary>
    /// <param name="date">date</param>
    /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    /// <summary>
    /// ThrowIfInvalidOptions 
    /// </summary>
    /// <param name="options"></param>
    private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
    {
      if (options.IsNull())
        throw new ArgumentNullException(nameof(options));

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
      }

      if (options.SigningCredentials.IsNull())
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
      }

      if (options.JtiGenerator.IsNull())
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
      }
    }
  }
}
