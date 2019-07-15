using System.Data;

namespace AP.Web.Persistence.UnitOfWork
{
  public interface IUnitOfWorkProvider
  {
    IUnitOfWork Provide(IUnitOfWorkDbOption dbOption, UnitOfWorkScopeOption scopeOption = UnitOfWorkScopeOption.Required,
        IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    IUnitOfWork Provide(UnitOfWorkScopeOption scopeOption = UnitOfWorkScopeOption.Required, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    // TODO: Create Provider for multiple db
    //IUnitOfWorkContainerScope ProvideContainer();
  }
}
