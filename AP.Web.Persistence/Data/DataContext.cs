using AP.Web.Common.Authentication;
using AP.Web.Persistence.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AP.Web.Persistence.Data
{
    /// <summary>
    /// DataContext class
    /// </summary>
    public class DataContext : IDataContext
    {
        public ApplicationDbContext AppDbContext { get; set; }

        public UserManager<AppUser> UserManager { get; set; }

        //public FacebookAuthSettings FbAuthSettings { get; set; }

        public IJwtFactory JwtFactory { get; set; }

        public JwtIssuerOptions JwtOptions { get; set; }
    }
}
