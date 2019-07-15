using AP.Web.Common.Authentication;
using AP.Web.Common.Extensions;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AP.Web.Common.Helpers
{
    public class Tokens
  {
    public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
    {
      var response = new
      {
        id = identity.Claims.Single(c => c.Type.IsEquals("id")).Value,
        auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
        expires_in = (int)jwtOptions.ValidFor.TotalSeconds
      };

      return JsonConvert.SerializeObject(response, serializerSettings);
    }
  }
}
