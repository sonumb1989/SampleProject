using AP.Web.Common.Authentication;
using AP.Web.Persistence.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AP.Web.Persistence.Data
{
    /// <summary>
    /// IDataContext interface
    /// </summary>
    public interface IDataContext
    {
        ApplicationDbContext AppDbContext { get; }

        UserManager<AppUser> UserManager { get; }

        //FacebookAuthSettings FbAuthSettings { get; }

        IJwtFactory JwtFactory { get; }

        JwtIssuerOptions JwtOptions { get; }
    }
}
