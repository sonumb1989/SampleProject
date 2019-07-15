using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AP.Web.Common.Constants
{
    public static class KeyConstants
    {
        #region Jwt Claim

        /// <summary>
        /// Init claims JWT Strings
        /// </summary>
        public static class Strings
        {
            public static readonly string IdentityToken = "Token";

            /// <summary>
            /// JwtClaimIdentifiers
            /// </summary>
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            /// <summary>
            /// JwtClaims
            /// </summary>
            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }

        #endregion

        #region [ Keys Constant for Status ]

        /// <summary>
        /// SecretKey
        // todo: get this from somewhere secure
        /// </summary>
        public static readonly string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";

        /// <summary>
        /// SigningKey
        /// </summary>
        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        /// <summary>
        /// MyAllowSpecificOrigins 
        /// </summary>
        public static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// MigrationsAssemblyName 
        /// </summary>
        public static readonly string MigrationsAssemblyName = "AP.Web.Persistence";

        /// <summary>
        /// DefaultConnection
        /// </summary>
        public static readonly string DefaultConnection = "DefaultConnection";

        /// <summary>
        /// ApiUser
        /// </summary>
        public static readonly string PolicyAuthoApiUser = "ApiUser";

        /// <summary>
        /// AccessControl
        /// </summary>
        public static readonly string AccessControl = "Access-Control-Allow-Origin";

        /// <summary>
        /// AssemblyServices
        /// </summary>
        public static readonly string AssemblyServices = "AP.Web.Services.Data";

        /// <summary>
        /// AssemblyAction
        /// </summary>
        public static readonly string AssemblyAction = "AP.Web.API.Action";

        /// <summary>
        /// OtherError
        /// </summary>
        public static readonly string OtherError = "255";

        #endregion [End Keys Constant for Status]
    }
}
