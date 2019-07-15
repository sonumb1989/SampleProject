using AP.Web.Persistence.UnitOfWork;

namespace AP.Web.Services.Common
{
    public interface IApplicationService
    {
        IUnitOfWorkProvider Provider { get; set; }
    }
}
